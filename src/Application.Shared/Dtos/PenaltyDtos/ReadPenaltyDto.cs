using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ReadPenaltyDto
    {
        public Guid PenaltyId { get; set; }
        public decimal? Amount { get; set; }
        public bool IsPaid { get; set; }
       
        public Guid? LoanId { get; set; }
        public string? MemberName { get; set; } // handled from BLL 
    }

}
