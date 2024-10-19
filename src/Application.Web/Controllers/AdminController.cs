using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        ///  Admin customized Dashboard
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
