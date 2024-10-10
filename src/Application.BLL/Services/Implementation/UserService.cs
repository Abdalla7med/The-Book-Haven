using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public class UserService : IUserService
    {
        public Task BlockUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadUserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReadUserDto> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadUserDto>> GetUsersByRoleAsync(string role)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
