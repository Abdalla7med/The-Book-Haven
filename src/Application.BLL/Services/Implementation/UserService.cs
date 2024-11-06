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
    /// Must Register this with 
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

        // Validate the required fields
        if (string.IsNullOrWhiteSpace(userCreateDto.FirstName) )
        {
            throw new ArgumentException("FirstName and LastName are required.");
        }

        // Map DTO to ApplicationUser
        var newUser = new ApplicationUser()
        {
            FirstName = userCreateDto.FirstName,
            Email = userCreateDto.Email,
            UserName = userCreateDto.UserName,
            ImageUrl = userCreateDto.ImageURL, // This can be null or empty
            IsPremium = userCreateDto.IsPremium,
            Role = userCreateDto.Role
        };

        newUser.IsPremium = isFacultyEmail; // Set IsPremium based on email domain

        // Create the user in Identity.
        IdentityResult result = await _userManager.CreateAsync(newUser, userCreateDto.Password);

        if (!result.Succeeded)
        {
            // Instead of throwing, you could log the error or return it as part of the result
            return new ApplicationResult() { Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList()};
        }

        // Assign the user role
        if (!string.IsNullOrEmpty(userCreateDto.Role))
        {
            if (!await _roleManager.RoleExistsAsync(userCreateDto.Role))
            {
                new ApplicationResult() { Succeeded = false, Errors = new List<string>() { "Role doesn't exists" } };
            }
            /// this will create user role 
            await _userManager.AddToRoleAsync(newUser, userCreateDto.Role);
        }
        // save multiple operations 
        await _unitOfWork.CompleteAsync();

        return new ApplicationResult(result);
    }

    // Login User
    public async Task<ApplicationResult> LoginUserAsync(string UserNameOrEmail, string password)
    {
        // Check if the input is an email or username
         var user = await _userManager.FindByEmailAsync(UserNameOrEmail);

         var User = await _userManager.FindByNameAsync(UserNameOrEmail);

        if (user == null && User == null)
        {
            return new ApplicationResult() { Succeeded = false , Errors = new List<string>() { "Invalid User Credentials" } }; // User not found or is marked as deleted
        }

        SignInResult result;

        if (user != null)
        {
           result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            return new ApplicationResult() { Succeeded = result.Succeeded };

        }

        result = await _signInManager.PasswordSignInAsync(User, password, isPersistent: false, lockoutOnFailure: false);
        return new ApplicationResult() { Succeeded = result.Succeeded };


    }

    // Update User Info
    public async Task<ApplicationResult> UpdateUserAsync(Guid userId, UpdateUserDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null || user.IsDeleted)
        {
            return new ApplicationResult() { Succeeded = false, Errors = new List<string> { "User Not Found" } };
        }

        // Update user properties
        user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
        user.Email = userUpdateDto.Email ?? user.Email;

        // Update the user in Identity
        IdentityResult result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new ApplicationResult() { Succeeded = false, Errors = result.Errors.Select(e => e.Description)};
        }

        await _unitOfWork.CompleteAsync();

        return new ApplicationResult(result); /// mapping from IdentityResult to our ApplicationCustomResult
    }

    // Soft Delete User (Only if no loans or unpaid penalties)
    public async Task<ApplicationResult> SoftDeleteUserAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId); // Load navigation properties

        if (user == null || user.IsDeleted)
        {
            return new ApplicationResult() { Succeeded = false, Errors = new List<string> { "User Not Found" } };
        }

        // Check if the user has active loans or unpaid penalties
        var hasLoans = user.Loans.Any(l => !l.IsReturned);
        var hasUnpaidPenalties = user.Penalties.Any(p => !p.IsPaid);

        if (hasLoans || hasUnpaidPenalties)
        {
            return new ApplicationResult { Succeeded = false, Errors = new List<string> { "User cannot be deleted due to active loans or unpaid penalties." } };
        }

        user.IsDeleted = true; // Mark user as deleted
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new ApplicationResult { Succeeded = false, Errors = result.Errors.Select(er => er.Description).ToList() };
        }

        await _unitOfWork.CompleteAsync();

        return new ApplicationResult { Succeeded = true, Data = result };
    }
    public async Task<ReadUserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null || user.IsDeleted)
        {
            throw new KeyNotFoundException("User not found or deleted.");
        }

        var Dto = new ReadUserDto()
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            Email = user.Email,
            Role = user.Role,
            IsBlocked = user.IsBlocked,
            ImageUrl = user.ImageUrl,
            IsDeleted = user.IsDeleted,
            IsPremium = user.IsPremium
        };

        return Dto;
    }

    public async Task<IEnumerable<ReadUserDto>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        // Check if users is null or empty
        if (users == null || !users.Any())
        {
            return new List<ReadUserDto>(); // Return an empty list if no users found
        }
        /// Mapp manually 
        List<ReadUserDto> UserDtos = new List<ReadUserDto>();

        foreach(var user in  users)
        {
            if (!user.IsDeleted)
            {
                var Dto = new ReadUserDto()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    Role = user.Role,
                    IsBlocked = user.IsBlocked,
                    ImageUrl = user.ImageUrl,
                    IsPremium = user.IsPremium
                };
                UserDtos.Add(Dto);
            }

        }

        return UserDtos;
    }

    public async Task<IEnumerable<ReadUserDto>> GetUsersByRoleAsync(string role)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        var filteredUsers = users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();
        /// Mapp manually 
        List<ReadUserDto> UserDtos = new List<ReadUserDto>();
        foreach (var user in filteredUsers)
        {
            var Dto = new ReadUserDto()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                Role = user.Role,
                IsBlocked = user.IsBlocked,
                ImageUrl = user.ImageUrl,
                IsDeleted = user.IsDeleted,
                IsPremium = user.IsPremium
            };
            UserDtos.Add(Dto);

        }

        return UserDtos;
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
    /// Create User and Return it, For Admin Create User and Bulk Creation, and if there's any need to get user after creating it 
    /// for auto login purpose (not done using this way)
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    public async Task<ReadUserDto> CreateUserAsync(CreateUserDto userDto)
    {
        await RegisterUserAsync(userDto);

        var user = await _userManager.FindByEmailAsync(userDto.Email);

        /// manual mapping 
        var Dto = new ReadUserDto()
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            Email = user.Email,
            Role = user.Role,
            IsBlocked = user.IsBlocked,
            ImageUrl = user.ImageUrl,
            IsDeleted = user.IsDeleted,
            IsPremium = user.IsPremium
        };
        return Dto;

        //return _mapper.Map<ReadUserDto>(user);
    }

    /// <summary>
    ///  SignOut from the Logged in Account
    /// </summary>
    /// <returns></returns>
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }


}
