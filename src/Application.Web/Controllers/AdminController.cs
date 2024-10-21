using Application.BLL;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly ILoanService _loanService;
        private readonly IPenaltyService _penaltyService;
        public AdminController(IUserService userService, IBookService bookService, ILoanService loanService, IPenaltyService penaltyService)
        {
            _userService = userService;
            _bookService = bookService;
            _loanService = loanService;
            _penaltyService = penaltyService;
        }
    
        /// <summary>
        ///  Admin customized Dashboard
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }


        public async Task<IActionResult> AllBooks()
        {

            var books = await _bookService.AllBooks();

            return View(books);

        }

        public async Task<IActionResult> AllMembers()
        {
            var Members = await _userService.GetAllUsersAsync();

            return View(Members);
        }

        public async Task<IActionResult> AllLoans()
        {
            var loans = await _loanService.AllLoans();

            return View(loans);
        }
        
        public async Task<IActionResult> AllPenalties()
        {
            var penalties = await _penaltyService.AllPenalties();

            return View(penalties);
        }

        [HttpGet]
        public IActionResult CreateUser()
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto model, IFormFile Image)
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Please upload an image.");
                    return View(model);
                }
                // Register the user via the user service
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    // Redirect to home after successful registration
                    return RedirectToAction("Index", "Admin");
                }

                // Handle registration errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(model);
        }
    }
}
