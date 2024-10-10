using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class UpdateUserDto
    {
        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        // Roles (can modify roles if necessary)
        public string Roles { get; set; }

        // Block/unblock user (for admin use)
        public bool IsBlocked { get; set; }

        // Delete/unDelete user (for admin use)
        public bool IsDeleted { get; set; }

        // Optional: Update books for authors
        public List<Guid>? AuthoredBookIds { get; set; }

        // Optional: Update loans for members
        public List<Guid>? LoanIds { get; set; }

        // Optional: Update penalties for members
        public List<Guid>? PenaltyIds { get; set; }
    }

}
