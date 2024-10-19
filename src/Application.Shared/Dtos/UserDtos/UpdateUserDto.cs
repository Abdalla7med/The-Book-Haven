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

        [Required]
        public Guid Id { get; set; } // User's unique identifier

        /// Not Required, incase of no need to update them 
        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"(.*\.(jpg|png)$)", ErrorMessage = "Please provide a valid image URL ending with .jpg or .png.")]
        public string ImageURL { get; set; } // Image file must be a valid URL, .jpg or .png only.

        public bool IsPremium { get; set; } // Can be updated based on certain conditions

        public bool IsDeleted { get; set; } // Soft delete property

        public bool IsBlocked { get; set; } // Block property to disable user access
    }

}
