using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
