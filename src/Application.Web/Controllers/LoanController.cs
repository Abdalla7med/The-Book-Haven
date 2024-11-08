using Application.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IPenaltyService _penaltyService;

        public LoanController(ILoanService loanService, IPenaltyService penaltyService, IUserService userService)
        {
            _loanService = loanService;
            _userService = userService;
            _penaltyService = penaltyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> ReturnLoan(Guid LoanId)
        {
            var result =await  _loanService.ReturnLoan(LoanId);
            if (result.Succeeded)
            {
                return RedirectToAction("Index",controllerName:"Home");
            }

            ModelState.AddModelError("",result.Errors.ToString());
            return RedirectToAction(actionName: "MyLoans", controllerName: "Member");
        }

        
    }
}
