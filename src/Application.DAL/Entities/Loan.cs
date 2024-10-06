using Application.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Loan : ISoftDeleteable
    {
        public int LoanId { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsDeleted { set; get; } = false;

        /// relations
        public int BookId { get; set; }
        public Book Book { get; set; }
        /// <summary>
        ///  IdentityUser Id is a GUID ( string ) 
        /// </summary>
        public string? MemberId { get; set; }
        public ApplicationUser? Member { get; set; }
        public Penalty? Penalty { get; set; }
    }
}
