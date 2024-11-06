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
        public async Task <ApplicationResult>AddBook(CreateBookDto createBookDto)
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
                return new ApplicationResult() { Succeeded = false, Errors = new List<string> { $"Author '{createBookDto.AuthorName}' Doesn't Exist" } };
            }


            // Publication year validation
            if (createBookDto.PublicationYear > DateTime.UtcNow.Year)
            {
                return new ApplicationResult() { Succeeded = false, Errors = new List<string> { "Publication Year is Not Valid" } };

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

            return new ApplicationResult() { Succeeded = true };
        }

        /// <summary>
        /// return all books and map it into BookDto using AutoMapper
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ReadBookDto>> AllBooks()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            var bookDtos = new List<ReadBookDto>();

            foreach (var book in books)
            {
                if (!book.IsDeleted)
                {
                    var dto = new ReadBookDto
                    {
                        Id = book.BookId,
                        Title = book.Title,
                        CoverUrl = book.CoverUrl,
                        ISBN = book.ISBN,
                        PublicationYear = book.PublicationYear,
                        AvailableCopies = book.AvailableCopies,
                        AuthorName = book.Author?.FirstName,     // Assuming Author is a navigation property
                        IsDeleted = book.IsDeleted
                    };


                    bookDtos.Add(dto);
                }
            }

            return bookDtos;

        }

        /// <summary>
        ///  Get Books in Form of Paginated List 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="category"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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

            List<ReadBookDto> BookDtos= new List<ReadBookDto>();
            foreach(var q in query)
            {
                if(!q.IsDeleted)
                {
                    var dto = new ReadBookDto
                    {
                        Id = q.BookId,
                        Title = q.Title,
                        CoverUrl = q.CoverUrl,
                        ISBN = q.ISBN,
                        PublicationYear = q.PublicationYear,
                        AvailableCopies = q.AvailableCopies,
                        AuthorName = q.Author?.FirstName,     // Assuming Author is a navigation property
                        IsDeleted = q.IsDeleted
                    };


                    BookDtos.Add(dto);
                }
            }

            // Paginate the results using PaginatedList
            return await Task.FromResult(PaginatedList<ReadBookDto>.Create(BookDtos.AsQueryable(), pageIndex, pageSize));
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
            var Dto = new ReadBookDto()
            {
                Id = book.BookId,
                Title = book.Title,
                AuthorName = book.Author.FirstName,
                AvailableCopies = book.AvailableCopies,
                CoverUrl = book.CoverUrl
            };
            // Using AutoMapper to map to ReadBookDto
            return Dto;
        }

        public async Task<IEnumerable<ReadBookDto>> GetBooksByAuthor(Guid authorId)
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();

            var AuthoredBooks = books.Where(b => b.AuthorId == authorId).ToList();

            var AuthoredBooksDtos = new List<ReadBookDto>();
            foreach (var book in AuthoredBooks) {
                var Dto = new ReadBookDto()
                {
                    Id = book.BookId,
                    Title = book.Title,
                    AuthorName = book.Author.FirstName,
                    AvailableCopies = book.AvailableCopies,
                    CoverUrl = book.CoverUrl
                };

                AuthoredBooksDtos.Add(Dto);
            }
            return AuthoredBooksDtos;
        }
        public async Task<PaginatedList<ReadBookDto>> GetAuthoredBooksAsync(Guid authorId, string searchTerm, string category, int pageIndex, int pageSize)
        {
            var books = _unitOfWork.BookRepository.GetAllAsQueryable();

            books = books.Where(b => b.AuthorId == authorId);

            // Apply filtering if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()));
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
        public async Task<ApplicationResult> UpdateBook(UpdateBookDto updateBookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(updateBookDto.BookId);
            if (book == null)
            {
                return new ApplicationResult() { Succeeded = false, Errors = new List<string> { "Book Not Found" } };
            }

            if (book.IsDeleted)
            {
                return new ApplicationResult() { Succeeded = false, Errors = new List<string> { "Cannot update a deleted book" } };
            }

           
            book.AvailableCopies = updateBookDto.AvailableCopies;

            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();

            return new ApplicationResult() { Succeeded = true };


        }

        public async Task<ApplicationResult> SoftDeleteBookAsync(Guid BookID)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(BookID);
            
            if(book== null)
                return new ApplicationResult() { Succeeded = false, Errors = new List<string>() { "Book not founded" } };

            book.IsDeleted = true;

            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();
            return new ApplicationResult() { Succeeded = true };

        }
    }
}
 