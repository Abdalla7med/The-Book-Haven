using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class MemberController : Controller
    {
        /// <summary>
        ///  Admin Customized Dashboard
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
