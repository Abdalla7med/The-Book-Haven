using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { set; get; }
    }
}
