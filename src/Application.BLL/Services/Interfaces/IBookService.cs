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
        Task AddBook(CreateBookDto createBookDto);
        Task<IEnumerable<ReadBookDto>> AllBooks();
        Task<ReadBookDto> GetBookById(Guid bookId);
        Task UpdateBook(UpdateBookDto updateBookDto);
        Task DeleteBook(Guid bookId);   
        Task<bool> IsBookAvailable(Guid bookId); // Check for both delted and available copies exists
        
    }
}
