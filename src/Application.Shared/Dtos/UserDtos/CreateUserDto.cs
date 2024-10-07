using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Roles (author, member, admin)
        [Required]
        public string Role { get; set; }

        // Author-specific properties
        public List<int>? AuthoredBookIds { get; set; }  // IDs of books the author has written

        // Member-specific properties (loans/penalties are added later when they occur)
    }

}
