using Application.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ReadUserDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        //public string LastName { get; set; }
        
        public string? UserName { set; get; }
        public string? Email { get; set; }

        // Roles
        public string? Role { set; get; }
        public bool IsBlocked { get; set; } // Whether the user is blocked (applies to members)
        public bool IsDeleted { get; set; }
        public bool IsPremium { set; get; }

        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"\w+\.(jpg|png|jpeg)", ErrorMessage = "The image URL must be a valid .png or .jpg file")]
        public string? ImageUrl {set; get;}
    }
}