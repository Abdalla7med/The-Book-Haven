using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    [Authorize(Roles = "Member" )]
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
         
        // books, loans, penalties, loans( returned) 

    }
}
