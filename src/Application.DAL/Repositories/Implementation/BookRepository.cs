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

        public override async Task<Book> GetByIdAsync(Guid id)
        {
            return await _dbset.Include(b => b.Loans)  // Include related Loans
                               .FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
