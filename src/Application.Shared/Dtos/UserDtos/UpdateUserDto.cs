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
        public Guid Id { get; set; } 

        [StringLength(25)]
        public string? FirstName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public bool? IsPremium { get; set; } // Can be updated based on certain conditions

        public bool? IsDeleted { get; set; } = false; // Soft delete property

        public bool? IsBlocked { get; set; } = false; // Block property to disable user access
    }

}
