using Application.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class PenaltyController : Controller
    {
        private readonly IPenaltyService _penaltyService;
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;

        public PenaltyController(IPenaltyService penaltyService, ILoanService loanService, IUserService userService)
        {
            _penaltyService = penaltyService;
            _userService = userService;
            _loanService = loanService;
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Pay(Guid PenaltyId)
        {
            var penalty = await _penaltyService.GetPenaltyById(PenaltyId);

            if(penalty == null)
            {
                ModelState.AddModelError("", "Penalty Doesn't Exists");
                return RedirectToAction(controllerName: "Member", actionName: "MyPenalties");
            }
          
            var result =  await _penaltyService.PayPenalty(penalty.LoanId ?? Guid.NewGuid(), penalty.Amount ?? 0);

            if (!result.Succeeded)
                ModelState.AddModelError("", result.Errors.ToString());

            return RedirectToAction(controllerName: "Member", actionName: "MyPenalties");
        }
         
    }

}
