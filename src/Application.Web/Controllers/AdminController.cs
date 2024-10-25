using Application.BLL;
using Application.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly ILoanService _loanService;
        private readonly IPenaltyService _penaltyService;
        private readonly IMapper _mapper;
        public AdminController(IUserService userService, IBookService bookService, ILoanService loanService, IPenaltyService penaltyService, IMapper mapper
            )
        {
            _userService = userService;
            _bookService = bookService;
            _loanService = loanService;
            _penaltyService = penaltyService;
            _mapper = mapper;
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
            var users = await _userService.GetAllUsersAsync(); // Assuming this returns IEnumerable<ApplicationUser>
          
            return View("AllMember",users);
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
                    return RedirectToAction("Dashboard", "Admin");
                }

                // Handle registration errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(model);
        }

        //// GET: Admin/EditBook/{id}
        //public async Task<IActionResult> EditBook(Guid id)
        //{
        //    var book = await _bookService.GetBookById(id); // Fetch the book details using the service
        //    if (book == null)
        //    {
        //        return NotFound(); // Return 404 if the book does not exist
        //    }

        //    // Map the book entity to the UpdateBookDto
        //    var updateBookDto = new UpdateBookDto
        //    {
        //        BookId = book.Id,
        //        CoverUrl = book.CoverUrl,
        //        AvailableCopies = book.AvailableCopies ?? 0,
        //        IsDeleted = book.IsDeleted
        //    };

        //    return View(updateBookDto); // Return the Edit view with the book details
        //}

        //// POST: Admin/EditBook
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditBook(UpdateBookDto model) // Accept the DTO as a parameter
        //{
        //    if (ModelState.IsValid) // Validate the model
        //    {
        //        await _bookService.UpdateBook(model); // Call the service to update the book
        //        TempData["SuccessMessage"] = "Book updated successfully."; // Optionally set a success message
        //        return RedirectToAction("AllBooks"); // Redirect to the AllBooks view after updating
        //    }

        //    // If validation fails, return the view with the current model to show errors
        //    return View(model);
        //}


        // POST: Admin/DeleteBook/{id}
        [HttpPost]
        [ValidateAntiForgeryToken] // To protect against CSRF attacks
        public IActionResult DeleteBook(Guid id)
        {
            var result = _bookService.DeleteBook(id); // Call the service to delete the book
            return RedirectToAction("AllBooks");
        }

        // GET: Admin/EditLoan/{id}
        public async Task<IActionResult> EditLoan(Guid id)
        {
            var loan = await _loanService.GetLoanById(id); // Fetch the loan details using the service
            if (loan == null || loan.IsReturned)
            {
                return NotFound(); // Return 404 if loan does not exist or is deleted
            }

            // Map the loan entity to the UpdateLoanDto
            var updateLoanDto = new UpdateLoanDto
            {
                LoanId = loan.LoanId,
                DueTime = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                IsReturned = loan.IsReturned,
            };

            return View(updateLoanDto); // Return the Edit view with the loan details
        }

        // POST: Admin/EditLoan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLoan(UpdateLoanDto model) // Accept the DTO as a parameter
        {
            if (ModelState.IsValid) // Validate the model
            {
                await _loanService.UpdateLoan(model); // Call the service to update the loan
                TempData["SuccessMessage"] = "Loan updated successfully."; // Optionally set a success message
                return RedirectToAction("AllLoans"); // Redirect to the AllLoans view after updating

            }
            // If validation fails or update fails, return the view with the current model to show errors
            return View(model);
        }

        // GET: Admin/EditUser/{id}
        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id); // Fetch user by ID
            if (user == null)
            {
                return NotFound(); // Return 404 if user not found
            }

            // Map ReadUserDto to UpdateUserDto for editing
            var updateUserDto = new UpdateUserDto
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                Email = user.Email,
                IsDeleted = user.IsDeleted,
                IsBlocked = user.IsBlocked,
                IsPremium = user.IsPremium
            };

            return View(updateUserDto); // Return the view with the user data
        }

        // POST: Admin/EditUser
        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateUserDto); // Return the view with validation errors
            }


            var Result = await _userService.UpdateUserAsync(updateUserDto.Id, updateUserDto); // Update user
            if (Result.Succeeded)
            {
                return RedirectToAction("AllMembers"); // Redirect to AllMembers after success
            }

            return View(updateUserDto);
        }

        // POST: Admin/DeleteUser/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(Guid id)
        {  
            await _userService.SoftDeleteUserAsync(id); // Call the delete method in the service
            return RedirectToAction("AllMembers"); // Redirect to AllMembers after success
        }

       
    }

}
