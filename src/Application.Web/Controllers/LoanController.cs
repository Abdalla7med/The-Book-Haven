using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// There'no Enity in the Views will be corresponding to loan just loan will created as service 
    }
}
