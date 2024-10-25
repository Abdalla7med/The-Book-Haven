using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DAL;
namespace Application.Shared
{
    public class  ReadLoanDto
    {
        public Guid LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } // once it's returned mark the IsDeleted with True 
        public string BookTitle { get; set; } 
        public string MemberName { get; set; }
        public  Penalty Penalty { get; set; }
    }

}
