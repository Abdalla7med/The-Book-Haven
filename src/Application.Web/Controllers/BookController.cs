using Application.BLL;
using Microsoft.AspNetCore.Mvc;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
namespace Application.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;    

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ReadBookDto> Books = await _bookService.AllBooks();
            /// sending books to view 
            return View("BookIndex", Books);
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound(); /// inCase of book not found 

            return View(book);
        }

        [HttpPost]
        /// Called from the ' Edit(Guid id) '
        public async Task<IActionResult> Edit(UpdateBookDto book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            await _bookService.DeleteBook(id);
            /// Return to index page 
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
