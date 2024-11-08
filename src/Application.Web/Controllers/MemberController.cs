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
        private readonly IPenaltyService _penaltyService;

        public MemberController(IUserService userService, ILoanService loanService, IPenaltyService penaltyService)
        {
            _userService = userService;
            _loanService = loanService;
            _penaltyService = penaltyService;
        }

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
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> MyLoans()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // Add error to ModelState for not being logged in
                ModelState.AddModelError("", "User is not logged in.");
                return RedirectToAction(controllerName: "Home", actionName: "Index");
            }

            Guid userID;
            // Try to parse the user ID to Guid
            if (!Guid.TryParse(userId, out userID))
            {
                // Add error to ModelState for invalid GUID format
                ModelState.AddModelError("", "Invalid user ID format.");
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            }

            var loans = await _loanService.GetLoansByMember(userID);
            if (loans == null)
            {
                // Handle the case where GetLoansByMember returns null
                ModelState.AddModelError("", "No loans found for the user.");
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            }

            loans = loans.Where(l => !l.IsReturned).ToList();

            ViewBag.UserName = User.Identity.Name;

            return View(loans);
        }

        [HttpGet]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> MyPenalties()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // Add error to ModelState for not being logged in
                ModelState.AddModelError("", "User is not logged in.");
                return RedirectToAction(controllerName: "Home", actionName: "Index");
            }

            Guid userID;
            // Try to parse the user ID to Guid
            if (!Guid.TryParse(userId, out userID))
            {
                // Add error to ModelState for invalid GUID format
                ModelState.AddModelError("", "Invalid user ID format.");
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            }

            var penalties = await _penaltyService.GetPenaltiesByMember(userID);
            if (penalties == null)
            {
                // Handle the case where GetLoansByMember returns null
                ModelState.AddModelError("", "No loans found for the user.");
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            }

            penalties = penalties.Where(l => !l.IsPaid).ToList();

            ViewBag.UserName = User.Identity.Name;

            return View(penalties);
        }

    }
}
