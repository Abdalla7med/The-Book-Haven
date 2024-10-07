using Application.DAL.Context;
using Application.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(BookHavenContext _context):base(_context) { }
        public async Task<ApplicationUser> GetByIdAsync(Guid userId)  // Overloaded method accepting GUID
        {
            return await _dbset.Include(U => U.BooksAuthored)
                               .Include(U => U.Penalties)
                               .Include(U => U.Loans)
                               .FirstOrDefaultAsync(U => U.Id == userId.ToString());
        }
    }
}
