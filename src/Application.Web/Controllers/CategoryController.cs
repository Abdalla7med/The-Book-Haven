using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
