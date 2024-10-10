using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class UpdatePenaltyDto
    {
        [Required]
        public Guid PenaltyId { get; set; }
        public bool IsPaid { get; set; }

        // Admin controls soft-delete
        public bool IsDeleted { get; set; }  // Admin-specific field for soft delete
    }

}
