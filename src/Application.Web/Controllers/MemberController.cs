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

        // AlBooks with Search Feature , MyLoans, MyPenalties, MyLoans( returned) -> filter Query ( Where(l => l.IsReturned); 

        

    }
}
