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
        Task SoftDeleteUserAsync(Guid id);
        Task BlockUserAsync(Guid id);

    }
}
