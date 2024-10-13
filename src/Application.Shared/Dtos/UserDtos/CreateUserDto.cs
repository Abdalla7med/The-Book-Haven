using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    /// <summary>
    ///  as Register Dto
    /// </summary>
    public class CreateUserDto
    {
        [Required(ErrorMessage = " Please Enter First Name")]
        [MaxLength(25, ErrorMessage = "Frist Name Must be Less Than 25 Characters")]
        [MinLength(3, ErrorMessage = "First Name Must be Greater Than 2 Characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Please Enter LastName")]
        [MaxLength(25, ErrorMessage = "Last Name Must be Less Than 25 Characters")]
        [MinLength(3, ErrorMessage = " Last Name Must be Greater Than 2 Characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please Enter Email ")]
        [EmailAddress(ErrorMessage = "Email you've provide doesn't valid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Roles (author, member, admin)
        [Required]
        public string Role { get; set; }
        public bool IsPremium { get; set; }
       
    }

}
