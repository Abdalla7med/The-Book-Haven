using Application.DAL;
using Application.DAL.UnitOfWork;
using Application.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Application.BLL
{
    public class BookService : IBookService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task AddBook(CreateBookDto createBookDto)
        {
            /// Get Category using its Id 
            /// Id will based through drop down list the display is category_name, and the value is category_id 
            var Category = await  _unitOfWork.CategoryRepository.GetByIdAsync(createBookDto.CategoryId);

            /// Category Validation 
            if (Category != null)
            {
                /// get Authors via search by names 
                List<ApplicationUser> authors = new List<ApplicationUser>();
                foreach(var AuthorName in createBookDto.AuthorNames)
                {
                    ApplicationUser Author = await _unitOfWork.UserRepository.GetUserByNameAsync(AuthorName);

                    /// Author Existence validation 
                    if (Author == null)
                        throw new Exception($"Author {AuthorName} Doesn't Exists");
                    authors.Add(Author);

                }

                if (createBookDto.PublicationYear > DateTime.UtcNow.Year)
                    throw new Exception("PublicationYear Not Valid");

                var Book = new Book()
                {
                    Title = createBookDto.Title,
                    ISBN = createBookDto.ISBN,
                    IsDeleted = false,
                    CoverUrl = createBookDto.CoverUrl, 
                    Category = Category, 
                    CategoryId = Category.CategoryId,
                    Loans = new List<Loan>(),
                    AvailableCopies = createBookDto.AvailableCopies, 
                    Authors = authors,
                    PublicationYear = createBookDto.PublicationYear 
                };


                await _unitOfWork.BookRepository.AddAsync(Book);
                await _unitOfWork.CompleteAsync();
            }
            throw new Exception("Category Not Found");
            
        }

        public Task<IEnumerable<ReadBookDto>> AllBooks()
        {
            throw new NotImplementedException();
        }

        public Task DeleteBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ReadBookDto> GetBookById(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBookAvailable(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBookExists(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBook(UpdateBookDto updateBookDto)
        {
            throw new NotImplementedException();
        }
    }
}
