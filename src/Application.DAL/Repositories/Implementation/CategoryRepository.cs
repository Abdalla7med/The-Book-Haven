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

        /// <summary>
        ///  override Implementation, Category , Author 
        ///  override Implementation and behavior remain the same 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Category> GetByIdAsync(Guid id) => await _dbset.Include(C => C.Books) 
                                                                                  .FirstOrDefaultAsync(C => C.CategoryId == id);

        public async Task<Category> GetCategory(string name) => await _dbset.Include(C => C.Books)
                                                                            .FirstOrDefaultAsync(C => C.Name == name);

    }
}
