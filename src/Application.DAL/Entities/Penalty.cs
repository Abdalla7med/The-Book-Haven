using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        
        [Precision(3,3), DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public bool IsPaid { set; get; }

        // Relations
        public int? LoanId { get; set; }
        public Loan Loan { get; set; }

        public int? MemberId { set; get; }
        public Member Member { set; get; }
    }
}
