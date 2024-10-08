using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface IUserRepository:IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByIdAsync(Guid userId);  // Overloaded method accepting GUID
    }
}
