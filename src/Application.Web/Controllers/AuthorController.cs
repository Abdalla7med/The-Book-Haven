using Application.BLL;
using Application.DAL;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Web.Controllers
{
    [Authorize(Roles = "Author")]
    public class AuthorController : Controller
    {

        private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;

        //private readonly UserManager<ApplicationUser> _userManager;
        public AuthorController(IBookService bookService, UserManager<ApplicationUser> userManager, ICategoryService categoryService)
        {
            _bookService = bookService;
            _userManager = userManager;
            _categoryService = categoryService; 
        }


        /// <summary>
        ///  Author Customized Dashboard
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }

        /// AuthorController: Dashboard, AuthoredBooks, PublishBook.
        
        public async Task<IActionResult> AuthoredBooks()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var AuthoredBooks = await _bookService.GetBooksByAuthor(Guid.Parse(userId));

            return View(AuthoredBooks);
        }
        [HttpGet]
        public IActionResult PublishBook()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> PublishBook(CreateBookDto book, IFormFile coverImage)
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
            return View(model: book);

        }
    }
}
