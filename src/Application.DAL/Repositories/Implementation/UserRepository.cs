using Application.DAL.Context;
using Application.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<ApplicationUser> _userManager; // Identity UserManager for handling users
        /// <summary>
        ///  Injecting both Context, and UserManager for purpose of handling queries more efficient 
        ///  Complex Queries done by Context 
        ///  Simple Queries done by userManager
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="userManager"></param>

        public UserRepository(BookHavenContext _context ,UserManager<ApplicationUser> userManager)
        : base(_context)
        {
            _userManager = userManager;
        }

        public async Task BlockUserAsync(Guid id)
        {
            var ApplicationUser = await GetByIdAsync(id);
            if (ApplicationUser != null) 
            {
                throw new Exception("User Not Exists");
            }

            ApplicationUser.IsBlocked = true;
            _context.Update(ApplicationUser);
            await _context.SaveChangesAsync();
        }

        // Override the GetByIdAsync to load a user by GUID
        public override async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString()); 
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string name)
        {

            return await _dbset.Where(u => u.FirstName + " " + u.LastName == name); ;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return await _dbset.Where(u => u.Role == role)
                   .ToListAsync();

        }

        public Task SoftDeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
