using Application.BLL;
using Microsoft.AspNetCore.Mvc;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Application.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Application.Web.Models;
namespace Application.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly ILoanService _loanService;
        public BookController(IBookService bookService, ILogger<BookController> logger, ICategoryService categoryService, ILoanService loanService)
        {
            _bookService = bookService;
            _logger = logger;
            _categoryService = categoryService;
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> Index( string category, int page = 1, string searchTerm = "")
        {

            int pageSize = 10; // Number of books per page

            // Load all books if no filters are applied
            var paginatedBooks = await _bookService.GetBooksAsync(searchTerm, category, page, pageSize);

            

            // Pass the paginated list to the view
            return View("BookIndex", paginatedBooks);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        /// <summary>
        /// we need to handle Image Upload to upload into wwwroot
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto book, [FromForm] IFormFile coverImage)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            // Check if the cover image file is uploaded
            if (coverImage != null && coverImage.Length > 0)
            {
                // Generate a unique file name for the image
                var fileName = Guid.NewGuid() + Path.GetExtension(coverImage.FileName);

                // Define the path to save the image
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", fileName);
                
                // Save the image to wwwroot/images/books
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                /// adding the file name for image 
                book.CoverUrl = fileName;

                // Call the service to add the book with the image URL
                 await _bookService.AddBook(book);
                
            }

            ModelState.AddModelError("", "An error occurred while adding the book.");
            return View(model:book);
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            // Map to UpdateBookDto if necessary
            var updateBookDto = new UpdateBookDto
            {
                BookId = book.Id,
                AvailableCopies = book.AvailableCopies ?? 0,
           };

            return View("Edit",updateBookDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(UpdateBookDto model)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBook(model);
                return RedirectToAction("Index");
            }
            return View("Edit",model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

            // POST: Book/DeleteBook/{id}
        [HttpPost]
        [Authorize(Roles = "Admin,Author")] // Restrict access to Admin and Author roles
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            var result = await _bookService.SoftDeleteBookAsync(id); // Assume DeleteBookAsync is the method to delete a book
            if (result.Succeeded)
            {
                return RedirectToAction("Index"); // Redirect back to Index after deletion
            }

            ModelState.AddModelError("", "An error occurred while deleting the book.");
                return RedirectToAction("Index"); // Redirect to Index even if an error occurs
            }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public async Task<IActionResult> LoanBook(Guid bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null )
            if(string.IsNullOrEmpty(userId))
            {
                // Add error to ModelState for not being logged in
                ModelState.AddModelError("", "User is not logged in.");
                return View("Index"); // Return view with the error message
            }

            // Try to parse the user ID to Guid
            if (!Guid.TryParse(userId, out Guid userID))
            {
                // Add error to ModelState for invalid GUID format
                ModelState.AddModelError("", "Invalid user ID format.");
                return View("Index"); // Return view with the error message
            }

            var Book = await _bookService.GetBookById(bookId);
            if (Book == null || Book.AvailableCopies < 1)
            {
                ModelState.AddModelError("", "No Available Books to be Loaned");
                return View("Index");
            }


            CreateLoanViewModel model = new()
            {
                Book = Book,
                MemberId = userID

            };

            return View("LoanBook", model);
        }

        [Authorize(Roles ="Member")]
        [HttpPost]
        public async Task<IActionResult> LoanBook(CreateLoanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", ModelState.ValidationState.ToString());
                return View("LoanBook",model);
            }

            CreateLoanDto Dto = new CreateLoanDto()
            {
                BookId = model.Book.Id,
                MemberId = model.MemberId,
                DueDate = model.DueDate,
                LoanDate = model.LoanDate 
            };
            var Result = await _loanService.AddLoan(Dto);
            if (Result.Succeeded)
            {
                return View("Index");
            }

            ModelState.AddModelError("", "An error occurred while creating the loan.");
            return View("LoanBook", model); // Return to the form if an error occurs

        }
    }
}

