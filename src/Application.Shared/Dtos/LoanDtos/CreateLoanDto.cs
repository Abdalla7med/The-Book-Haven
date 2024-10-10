using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreateLoanDto
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public Guid MemberId { get; set; } // GUID, will be added by default through view model ( user account ) 
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }  // Calculate based on borrowing duration ( default is 5 days and may be other calculations will be added on the BLL ) 
    }

}
