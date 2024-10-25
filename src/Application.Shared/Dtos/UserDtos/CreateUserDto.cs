using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    /// <summary>
    ///  as Register Dto, with Server Side Dtos
    /// </summary>
    public class CreateUserDto
    {
        [Required(ErrorMessage = " Please Enter First Name")]
        [MaxLength(25, ErrorMessage = "Frist Name Must be Less Than 25 Characters")]
        [MinLength(3, ErrorMessage = "First Name Must be Greater Than 2 Characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        [UniqueUsername(ErrorMessage ="UserName Not Available")] /// Custom Validation Attribute
        public string UserName { set; get; }

        [Required(ErrorMessage ="Please Enter Email ")]
        [EmailAddress(ErrorMessage = "Email you've provide doesn't valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
    //    [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
       // [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and Confirm Password must be Identical")]
        public string ConfirmPassword { set; get; }

        // Roles (author, member, admin)
        //[Required]
        [RegularExpression("Admin|Member|Author", ErrorMessage = "Role must be either 'Admin', 'Member', or 'Author'.")]
        public string? Role { get; set; }

        public bool IsPremium { get; set; }

        // [RegularExpression(@"(.*\.(jpg|png|jpeg)$)", ErrorMessage = "Please provide a valid image URL ending with .jpg or .png.")]
        public string? ImageURL { get; set; } = string.Empty; // Image file must be a valid URL, .jpg or .png only.

    }

}
