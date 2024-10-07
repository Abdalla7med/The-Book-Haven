using Application.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookHavenContext _context) : base(_context) { }

        public override async Task<Category> GetByIdAsync(int id)
        {
            return await _dbset.Include(C => C.Books)
                .FirstOrDefaultAsync(C => C.CategoryId == id);
        }
    }
}
