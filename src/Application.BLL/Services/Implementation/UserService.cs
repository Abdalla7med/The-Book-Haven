using Application.BLL;
using Application.DAL.UnitOfWork;
using Application.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Application.Shared;
using Application.Shared.Dtos.UserDtos;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly string facultyDomain = ".edu.eg"; // Example faculty domain

    public UserService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    // Register User
    public async Task<ApplicationResult> RegisterUserAsync(CreateUserDto userCreateDto)
    {
        // Check if the email belongs to the faculty domain
        bool isFacultyEmail = userCreateDto.Email.EndsWith(facultyDomain, StringComparison.OrdinalIgnoreCase);

        // Map DTO to ApplicationUser
        var newUser = _mapper.Map<ApplicationUser>(userCreateDto);
        newUser.IsPremium = isFacultyEmail; // Set IsPremium based on email domain

        // Create the user in Identity
        IdentityResult result = await _userManager.CreateAsync(newUser, userCreateDto.Password);

        if (!result.Succeeded)
        {
            throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Assign the user role
        if (!string.IsNullOrEmpty(userCreateDto.Role))
        {
            await _userManager.AddToRoleAsync(newUser, userCreateDto.Role);
        }

        await _unitOfWork.CompleteAsync();
        return new ApplicationResult(result);
    }

    // Login User
    public async Task<ApplicationResult> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || user.IsDeleted)
        {
            return new ApplicationResult() { Succeeded = false , Errors = new List<string>() { "Invalid User Credentials" } }; // User not found or is marked as deleted
        }

        SignInResult result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

        return new ApplicationResult() { Succeeded = result.Succeeded }; 
    }

    // Update User Info
    public async Task<ApplicationResult> UpdateUserAsync(Guid userId, UpdateUserDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null || user.IsDeleted)
        {
            throw new KeyNotFoundException("User not found or deleted.");
        }

        // Update user properties
        user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
        user.LastName = userUpdateDto.LastName ?? user.LastName;
        user.Email = userUpdateDto.Email ?? user.Email;

        // Update the user in Identity
        IdentityResult result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _unitOfWork.CompleteAsync();

        return new ApplicationResult(result); /// mapping from IdentityResult to our ApplicationCustomResult
    }

    // Soft Delete User (Only if no loans or unpaid penalties)
    public async Task SoftDeleteUserAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId); // Load navigation properties

        if (user == null || user.IsDeleted)
        {
            throw new KeyNotFoundException("User not found or deleted.");
        }

        // Check if the user has active loans or unpaid penalties
        var hasLoans = user.Loans.Any(l => !l.IsReturned);
        var hasUnpaidPenalties = user.Penalties.Any(p => !p.IsPaid);

        if (hasLoans || hasUnpaidPenalties)
        {
            throw new InvalidOperationException("User cannot be deleted due to active loans or unpaid penalties.");
        }

        user.IsDeleted = true; // Mark user as deleted
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _unitOfWork.CompleteAsync();
    }

    // Get User Info
    public async Task<ReadUserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null || user.IsDeleted)
        {
            throw new KeyNotFoundException("User not found or deleted.");
        }

        return _mapper.Map<ReadUserDto>(user);
    }

    public async Task<IEnumerable<ReadUserDto>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReadUserDto>>(users);
    }

    public async Task<IEnumerable<ReadUserDto>> GetUsersByRoleAsync(string role)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        var filteredUsers = users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();
        return _mapper.Map<IEnumerable<ReadUserDto>>(filteredUsers);
    }

    public async Task BlockUserAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user == null)
            throw new ArgumentException("User doesn't exist");

        // Implement blocking logic, e.g., setting a 'IsBlocked' property
        user.IsBlocked = true; // Assuming there's a property for blocking users
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to block user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _unitOfWork.CompleteAsync();
    }

    /// <summary>
    /// Create User and Return it, For Admin Create User and Bulk Creation 
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    public async Task<ReadUserDto> CreateUserAsync(CreateUserDto userDto)
    {
        await RegisterUserAsync(userDto);

        var user = await _userManager.FindByEmailAsync(userDto.Email);

        return _mapper.Map<ReadUserDto>(user);
    }

    /// <summary>
    ///  SignOut from the Logged in Account
    /// </summary>
    /// <returns></returns>
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    /// <summary>
    ///  Change Password 
    /// </summary>
    /// <param name="changePasswordDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to change password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }

        // Generate password reset token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // Here you would normally send the token to the user's email
        // You can implement an email service to handle that

        return true; // Indicate that the password reset email has been sent
    }

  

    public async Task<ApplicationResult> DeleteUserAsync(Guid userId)
    {
        await _unitOfWork.UserRepository.DeleteAsyncById(userId);

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user == null || user.IsDeleted)
            return new ApplicationResult() { Succeeded = true };

        return new ApplicationResult() { Succeeded = false,  };


    }

}
