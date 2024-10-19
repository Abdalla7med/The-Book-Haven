using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class AuthorController : Controller
    {
        /// <summary>
        ///  Author Customized Dashboard
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
