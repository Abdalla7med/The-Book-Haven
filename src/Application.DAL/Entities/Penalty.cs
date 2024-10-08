using Application.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Penalty : ISoftDeleteable
    {
        public Guid PenaltyId { get; set; } = Guid.NewGuid();
        
        [Precision(3,3), DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public bool IsPaid { set; get; }
        public bool IsDeleted { set; get; } = false;
        // Relations
        public Guid? LoanId { get; set; }
        public Loan? Loan { get; set; }
        /// <summary>
        /// IdentityUser ID is a GUID 
        /// </summary>
        public Guid? MemberId { set; get; }
        public ApplicationUser? Member { set; get; }
    }
}
