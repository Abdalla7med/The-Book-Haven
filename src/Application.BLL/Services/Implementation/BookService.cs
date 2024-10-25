using Application.DAL;
using Application.DAL.UnitOfWork;
using Application.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.BLL
{
   
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Adding Book Logic
        /// Book must placed in Category
        /// Author must exist in the system
        /// </summary>
        /// <param name="createBookDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddBook(CreateBookDto createBookDto)
        {
            // Get Category using its Id
          /*  var category = await _unitOfWork.CategoryRepository.GetByIdAsync(createBookDto.CategoryId);

            // Category validation
            if (category == null)
            {
                throw new ArgumentException("Category doesn't exists");  
            }
            */
            // Get authors by searching their names
           
            var author = await _unitOfWork.UserRepository.GetUserByNameAsync(createBookDto.AuthorName);

            // Author existence validation
            if (author == null)
            {
                throw new ArgumentException($"Author '{createBookDto.AuthorName}' Doesn't Exist");
            }


            // Publication year validation
            if (createBookDto.PublicationYear > DateTime.UtcNow.Year)
            {
                throw new ArgumentException("Publication Year is Not Valid");
            }

            var book = new Book
            {
                Title = createBookDto.Title,
                ISBN = createBookDto.ISBN,
                IsDeleted = false,
                CoverUrl = createBookDto.CoverUrl,
                Loans = new List<Loan>(),
                AvailableCopies = createBookDto.AvailableCopies,
                Author = author,
                PublicationYear = createBookDto.PublicationYear
            };


            // Add book and save changes
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// return all books and map it into BookDto using AutoMapper
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ReadBookDto>> AllBooks()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            // Using AutoMapper to map
            return _mapper.Map<IEnumerable<ReadBookDto>>(books);
        }

        public async Task<PaginatedList<ReadBookDto>> GetBooksAsync(string searchTerm, string category, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.BookRepository.GetAllAsQueryable();

            // Filtering based on searchTerm and category
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(b => b.Category.Name == category);
            }

            // Project to DTO (assuming you have a ReadBookDto class)
            var booksDtoQuery = query.Select(book => new ReadBookDto
            {
                Id = book.BookId,
                Title = book.Title,
                AuthorName = book.Author.FirstName,
                AvailableCopies = book.AvailableCopies,
                CoverUrl = book.CoverUrl
            });

            // Paginate the results using PaginatedList
            return await Task.FromResult(PaginatedList<ReadBookDto>.Create(booksDtoQuery, pageIndex, pageSize));
        }

        /// <summary>
        /// Delete a book using Soft Delete 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteBook(Guid bookId)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException("Book Not Found");
            }

            // Soft delete by marking the book as deleted
            book.IsDeleted = true;
            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Search For book by it's id 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ReadBookDto> GetBookById(Guid bookId)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException("Book Not Found");
            }

            // Using AutoMapper to map to ReadBookDto
            return _mapper.Map<ReadBookDto>(book);
        }

        public async Task<IEnumerable<ReadBookDto>> GetBooksByAuthor(Guid authorId)
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();

            var AuthoredBooks = books.Where(b => b.AuthorId == authorId).ToList();

            return _mapper.Map<IEnumerable<ReadBookDto>>(AuthoredBooks);

        }

        public async Task<PaginatedList<ReadBookDto>> GetAuthoredBooksAsync(Guid authorId, string searchTerm, string category, int pageIndex, int pageSize)
        {
            var books = _unitOfWork.BookRepository.GetAllAsQueryable();

            books = books.Where(b => b.AuthorId == authorId);

            // Apply filtering if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Apply filtering if category is provided
            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(b => b.Category.Name == category);
            }

            var booksDtoQuery = books.Select(book => new ReadBookDto
            {
                Id = book.BookId,
                Title = book.Title,
                AuthorName = book.Author.FirstName,
                AvailableCopies = book.AvailableCopies,
                CoverUrl = book.CoverUrl
            });

            // Create a paginated list
            return await Task.FromResult(PaginatedList<ReadBookDto>.Create(booksDtoQuery, pageIndex, pageSize));
        }

        /// <summary>
        /// Check if book has available copies, and not deleted
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> IsBookAvailable(Guid bookId)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException("Book Not Found");
            }

            // Return if the book is available and not deleted
            return book.AvailableCopies > 0 && !book.IsDeleted;
        }

        /// <summary>
        /// Update Book Details 
        /// </summary>
        /// <param name="updateBookDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"> the first to be handled through the catch block</exception>
        /// <exception cref="InvalidOperationException">the second to be handled through the catch block</exception>
        public async Task UpdateBook(UpdateBookDto updateBookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(updateBookDto.BookId);
            if (book == null)
            {
                throw new ArgumentException("Book Not Found");
            }

            if (book.IsDeleted)
            {
                throw new InvalidOperationException("Cannot update a deleted book");
            }

            // Updating properties
            book.IsDeleted = updateBookDto.IsDeleted;
            book.CoverUrl = updateBookDto.CoverUrl; // Allow cover URL change
            book.AvailableCopies = updateBookDto.AvailableCopies;

            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();
        }
    }
}
 