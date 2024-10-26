using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public interface IBookService
    {
        Task<ApplicationResult> AddBook(CreateBookDto createBookDto);
        Task<IEnumerable<ReadBookDto>> AllBooks();
        Task<PaginatedList<ReadBookDto>> GetBooksAsync(string searchTerm, string category, int page, int pageSize);
        Task<ReadBookDto> GetBookById(Guid bookId);
        Task<ApplicationResult> UpdateBook(UpdateBookDto updateBookDto);
        Task DeleteBook(Guid bookId);   
        Task<bool> IsBookAvailable(Guid bookId); // Check for both deleted and available copies exists
        Task<IEnumerable<ReadBookDto>> GetBooksByAuthor(Guid authorId);
        Task<PaginatedList<ReadBookDto>> GetAuthoredBooksAsync(Guid authorId, string searchTerm, string category, int pageIndex, int pageSize);
    }
}
