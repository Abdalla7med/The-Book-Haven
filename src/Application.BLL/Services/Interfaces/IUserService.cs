﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
namespace Application.BLL
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDto>> GetAllUsersAsync();
        Task<ReadUserDto> GetUserByIdAsync(Guid id);
        Task<IEnumerable<ReadUserDto>> GetUsersByRoleAsync(string role);
        Task RegisterUserAsync(CreateUserDto userCreateDto);
        Task<bool> LoginUserAsync(string email, string password);
        Task UpdateUserAsync(Guid userId, UpdateUserDto userUpdateDto);
        Task SoftDeleteUserAsync(Guid id);
        Task BlockUserAsync(Guid id);
    }
}
