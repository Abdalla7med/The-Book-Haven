using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role);
        Task<ApplicationUser> GetUserByNameAsync(string name);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task BlockUserAsync(Guid id);
        // Load Books Authored by Author
        Task LoadBooksAuthored(Guid userId);
        // Load loans and penalties for a specific user (Member role)
        Task LoadLoansAndPenalties(Guid userId);

    }
}
