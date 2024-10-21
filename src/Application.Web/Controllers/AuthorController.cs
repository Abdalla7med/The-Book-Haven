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
        
        public async Task<IActionResult> AuthoredBooks()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var AuthoredBooks = await _bookService.GetBooksByAuthor(user.Id);

            return View(AuthoredBooks);
        }

        [HttpGet]
        public async Task<IActionResult> PublishBook()
        {
            // Assume you have a method to get categories
            var categories = await _categoryService.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name"); // Assuming Id and Name are properties

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> PublishBook(CreateBookDto book, IFormFile coverImage)
        {
            if (!ModelState.IsValid)
            {
                // Populate categories again for the view
                var categories1 = await _categoryService.AllCategories();
                ViewBag.Categories = new SelectList(categories1, "Id", "Name");

                return View(book);
            }
             // Set the AuthorName to the logged -in user's name
              book.AuthorName = User.Identity.Name;
            // Check if the cover image file is uploaded
            if (coverImage != null && coverImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(coverImage.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                book.CoverUrl = fileName;


                await _bookService.AddBook(book);
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "An error occurred while adding the book.");

            // Populate categories again for the view
            var categories = await _categoryService.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(book);
        }

    }
}
