using Application.BLL;
using Application.DAL;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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
        public async Task<IActionResult> Dashboard()
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var AuthoredBooks = await _bookService.GetBooksByAuthor(Guid.Parse(userId));

            ViewBag.WrittenBooks = AuthoredBooks.Count();

            return View();
        }

        /// AuthorController: Dashboard, AuthoredBooks, PublishBook.
        [HttpGet]
        public async Task<IActionResult> AuthoredBooks(string category, int page = 1, string searchTerm ="")
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var pageIndex = 10;

            var AuthoredBooks = await _bookService.GetAuthoredBooksAsync(user.Id, searchTerm, category,page, pageIndex);

            return View(AuthoredBooks);
        }

        [HttpGet]
        public IActionResult PublishBook()
        {
            // Retrieve categories from the service
            //var categories = await _categoryService.AllCategories();

            //// Check if categories were retrieved successfully
            //if (categories == null || !categories.Any())
            //{
            //    ModelState.AddModelError("", "No categories found.");
            //    return View();
            //}

            //ViewBag.Categories = new SelectList(categories, "CategoryId", "Name"); // Make sure these match your properties

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

            // Check if the cover image was uploaded
            if (coverImage != null && coverImage.Length > 0)
            {
                // Get the root path of the wwwroot folder
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                // Create a unique file name for the uploaded cover image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);

                // Define the path to save the cover image
                var filePath = Path.Combine(wwwRootPath, "images", "books", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.Combine(wwwRootPath, "images", "books"));

                // Save the cover image to wwwroot/images/books folder
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                // Set the CoverUrl in the model to use the relative path
                book.CoverUrl = "/images/books/" + fileName;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please upload a cover image.");
                return View(book);
            }

            // Add the book using the book service
            var result = await _bookService.AddBook(book);

            if (result.Succeeded)
            {
                // Redirect to Dashboard after successfully adding the book
                return RedirectToAction("Dashboard");
            }

            // Handle errors from adding the book
            ModelState.AddModelError("Saving Book Error", result?.Errors?.ToString() ?? string.Empty);
            return View(book);
        }
    }
}
