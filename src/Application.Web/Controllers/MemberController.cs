using Application.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Web.Controllers
{
    [Authorize(Roles = "Member" )]
    public class MemberController : Controller
    {

        private readonly IUserService _userService;
        private readonly ILoanService _loanService;

        /// <summary>
        ///  Admin Customized Dashboard
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }

        // AlBooks with Search Feature , MyLoans, MyPenalties, MyLoans( returned) -> filter Query ( Where(l => l.IsReturned); 
        [HttpGet]
        public async Task<IActionResult> MyLoans()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                if (string.IsNullOrEmpty(userId))
                {
                    // Add error to ModelState for not being logged in
                    ModelState.AddModelError("", "User is not logged in.");
                    return View("Index"); // Return view with the error message
                }

            // Try to parse the user ID to Guid
            if (!Guid.TryParse(userId, out Guid userID))
            {
                // Add error to ModelState for invalid GUID format
                ModelState.AddModelError("", "Invalid user ID format.");
                return View("Index"); // Return view with the error message
            }

            var loans = await _loanService.GetLoansByMember(userID);
            loans = loans.Where(l => !l.IsReturned).ToList();

            ViewBag.UserName = User.Identity.Name;

            return View(loans);

        }

    }
}
