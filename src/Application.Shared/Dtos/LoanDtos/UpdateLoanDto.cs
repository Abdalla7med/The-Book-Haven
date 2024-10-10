using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class UpdateLoanDto
    {
        [Required]
        public Guid LoanId { get; set; }
        public DateTime? ReturnDate { get; set; }  // Update return date if the book is returned
        public bool IsReturned { get; set; }

        // Admin controls soft-delete
        public bool IsDeleted { get; set; }  // Admin-specific field for soft delete
    }

}
