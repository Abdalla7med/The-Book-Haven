using Application.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    /// <summary> XML docs 
    ///  Application user will play three roles 
    ///  Admin : with all nav properties with null value
    ///  Member : with BooksAuthored nav with null value 
    ///  Author : with Loans, and Penalties with null value 
    ///  note that we must look at role, because might exist as member with no written books 
    ///  deleted or blocked will be via <LockoutEnd> LockoutEnd>property inherited from IdentityUser 
    /// </summary>
    public class ApplicationUser: IdentityUser<Guid>, ISoftDeleteable
    {
        [StringLength(25)]
        public string? FirstName { get; set; }

        [StringLength(25)]
        public string? LastName { get; set; }

        /// Soft Delete Properties 
        public bool IsDeleted { set; get; } = false;
        public bool IsBlocked { set; get; } = false; /// till  now we'll use it as a blocking property instead of LockoutEnd 

        [Required]
        /// Default Role is member 
        public string Role { set; get; } = "Member";
        public bool IsPremium { get; set; } = false;

        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "The image URL must be a valid .png or .jpg file")]
        public string? ImageUrl { set; get; }
        // Navigation property: List of books authored by this user (only applicable if the user is an "Author")
        public ICollection<Book>? BooksAuthored { get; set; }

        // Navigation property: List of loans (only applicable if the user is a "Member")
        public ICollection<Loan>? Loans { get; set; } = new List<Loan>();

        // Navigation property: List of penalties (only applicable if the user is a "Member")
        public ICollection<Penalty>? Penalties { get; set; } = new List<Penalty>();

       
    }
}
