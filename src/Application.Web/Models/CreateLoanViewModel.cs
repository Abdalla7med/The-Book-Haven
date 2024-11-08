using Application.Shared;

namespace Application.Web.Models
{
    public class CreateLoanViewModel
    {
        public ReadBookDto? Book { set; get; }
        public Guid MemberId { set; get; }
        // default is adding 7 days after the loan date
        [DateRangeValidation(7)]
        public DateTime DueDate { set; get; } = DateTime.UtcNow.AddDays(7);
        public DateTime LoanDate { set; get; } = DateTime.UtcNow;
    }
}
