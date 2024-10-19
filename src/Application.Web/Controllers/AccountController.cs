using Application.BLL;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController (IUserService userService)
        {
            _userService = userService;
        }

        // GET: User/Register
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    // Sign-in the user automatically after registration (optional)
                    // await _signInManager.SignInAsync(result.User, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(model);
        }



        // GET: User/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, String Password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(Email, Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(new LoginViewModel { Password = Password, Email = Email });
        }




        // POST: User/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            return RedirectToAction("Login", "User");
        }




        // GET: User/EditProfile
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            // Get the logged-in user ID from claims
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case where the user ID is not found in the claims
                return Unauthorized("User is not logged in.");
            }

            // Try to parse the user ID to Guid
            if (!Guid.TryParse(userId, out Guid userID))
            {
                // Handle invalid GUID format
                return BadRequest("Invalid user ID format.");
            }

            // Fetch user details from the service
            var user = await _userService.GetUserByIdAsync(userID);

            if (user == null)
            {
                // Handle the case where the user is not found in the database
                return NotFound("User not found.");
            }

            // Create the view model based on the fetched user details
            var model = new UpdateUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ImageURL = user.ImageUrl
            };

            // Return the EditProfile view with the model
            return View(model);
        }


        // POST: User/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UpdateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(model.Id, model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Failed to update profile.");
            }

            return View(model);
        }

        // POST: User/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault());
                return RedirectToAction("Profile");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: User/Details
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
