using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ReadPenaltyDto
    {
        public int PenaltyId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
       
        public int LoanId { get; set; }
        public string? MemberName { get; set; } // handled from BLL 
    }

}
