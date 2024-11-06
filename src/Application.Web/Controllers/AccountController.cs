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
        public async Task<IActionResult> Register(CreateUserDto model, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                // Check if the image was uploaded
                if (Image != null && Image.Length > 0)
                {
                    // Get the root path of the wwwroot folder
                    var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    // Create a unique file name for the uploaded image
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);

                    // Define the path to save the image
                    var filePath = Path.Combine(wwwRootPath, "uploads", "images", fileName);

                    // Ensure the directory exists
                    Directory.CreateDirectory(Path.Combine(wwwRootPath, "uploads", "images"));

                    // Save the image to wwwroot/uploads/images folder
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    // Set the ImageURL in the model
                    model.ImageURL = "/uploads/images/" + fileName;  // Relative path
                }

                if (model.ImageURL == null)
                    model.ImageURL = " ";

                // Register the user via the user service
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    // Redirect to home after successful registration
                    return RedirectToAction("Index", "Home");
                }

                // Handle registration errors
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
        public async Task<IActionResult> Login(string UserNameOrEmail, String Password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(UserNameOrEmail, Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty,result.Errors.ElementAt(0));
            }

            return View(new LoginViewModel { Password = Password, UserNameOrEmail = UserNameOrEmail });
        }




        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: User/EditProfile
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            // Get the logged-in user ID from claims
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // Add error to ModelState for not being logged in
                ModelState.AddModelError("", "User is not logged in.");
                return View("Edit"); // Return view with the error message
            }

            // Try to parse the user ID to Guid
            if (!Guid.TryParse(userId, out Guid userID))
            {
                // Add error to ModelState for invalid GUID format
                ModelState.AddModelError("", "Invalid user ID format.");
                return View("Edit"); // Return view with the error message
            }

            // Fetch user details from the service
            var user = await _userService.GetUserByIdAsync(userID);

            if (user == null)
            {
                // Add error to ModelState for user not found
                ModelState.AddModelError("", "User not found.");
                return View("Edit"); // Return view with the error message
            }

            // Create the view model based on the fetched user details
            var model = new UpdateUserDto
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                Email = user.Email,
            };

            // Return the EditProfile view with the model
            return View("Edit", model);
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

            return View("Edit",model);
        }

        // POST: User/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.SoftDeleteUserAsync(id);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault());
                return RedirectToAction("Profile");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
