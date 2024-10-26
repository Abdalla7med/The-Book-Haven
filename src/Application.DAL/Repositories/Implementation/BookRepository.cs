using Application.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class BookRepository:GenericRepository<Book>, IBookRepository
    {

        public BookRepository(BookHavenContext _context): base(_context) { }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbset.Include(b => b.Author)
                                .ToListAsync();
        }
        public async Task<Book> GetBookByNameAsync(string BookTitle)
        {
            return await _dbset.Include(b => b.Author)
                                .FirstOrDefaultAsync(b => b.Title == BookTitle);


        }

        public async Task<Book> GetBooksByCategoryAsync(string CategoryName)
        {
            return await _dbset.Include(b => b.Author).
                                FirstOrDefaultAsync(b => b.Category.Name == CategoryName);
        }

        public override async Task<Book> GetByIdAsync(Guid id)
        {
            return await _dbset.Include(b => b.Loans) 
                               .Include(b => b.Author)
                               .FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
