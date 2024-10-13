using Application.BLL;
using Application.DAL.UnitOfWork;
using Application.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Application.Shared;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly string facultyDomain = ".edu.eg"; // Example faculty domain

    public UserService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Register User
    public async Task RegisterUserAsync(CreateUserDto userCreateDto)
    {
        // Check if the email belongs to the faculty domain
        bool isFacultyEmail = userCreateDto.Email.EndsWith(facultyDomain, StringComparison.OrdinalIgnoreCase);

        // Map DTO to ApplicationUser
        var newUser = _mapper.Map<ApplicationUser>(userCreateDto);

        // Set IsPremium to true if faculty email is detected
        newUser.IsPremium = isFacultyEmail;

        // Create the user in Identity
        var result = await _userManager.CreateAsync(newUser, userCreateDto.Password);

        if (!result.Succeeded)
        {
            throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Assign the user role
        await _userManager.AddToRoleAsync(newUser, userCreateDto.Role);

        await _unitOfWork.CompleteAsync();
    }

    // Login User
    public async Task<bool> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || user.IsDeleted)
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

        return result.Succeeded;
    }

    // Update User Info
    public async Task UpdateUserAsync(Guid userId, UpdateUserDto userUpdateDto)
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

        // Update the user in the repository
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to update user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _unitOfWork.CompleteAsync();
    }

    // Delete User (Only if no loans or unpaid penalties)
    public async Task SoftDeleteUserAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId); /// to load Nav-Properties 

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
        var Users = await _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReadUserDto>>(Users);    
    }

    public async Task<IEnumerable<ReadUserDto>> GetUsersByRoleAsync(string role)
    {
        var Users = await _unitOfWork.UserRepository.GetAllAsync();
        Users = Users.Where(U => role == U.Role).ToList();

        return _mapper.Map<IEnumerable<ReadUserDto>>(Users);
    }


    public async Task BlockUserAsync(Guid id)
    {
        var User = await _unitOfWork.UserRepository.GetByIdAsync(id);

        if (User == null)
            throw new ArgumentException("User Doesn't exists");
    }
}
