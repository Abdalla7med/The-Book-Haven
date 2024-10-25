using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Shared;
using Microsoft.AspNetCore.Identity;
namespace Application.BLL
{
    public interface IUserService
    {
        Task<ReadUserDto> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<ReadUserDto>> GetAllUsersAsync();
        Task<ReadUserDto> CreateUserAsync(CreateUserDto userDto);
        Task<ApplicationResult> UpdateUserAsync(Guid userId, UpdateUserDto userDto);
        Task<ApplicationResult> SoftDeleteUserAsync(Guid userId);
        Task<ApplicationResult> RegisterUserAsync(CreateUserDto registerUserDto);
        Task<ApplicationResult> LoginUserAsync(string Email, string Password);
        Task SignOutAsync();
    }
}



