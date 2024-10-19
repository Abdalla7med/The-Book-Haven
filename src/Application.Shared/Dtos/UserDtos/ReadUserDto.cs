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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        // Roles
        public string Role { set; get; }

        // Author-specific properties
        public List<ReadBookDto>? BooksAuthored { get; set; }  // Books authored by this user

        // Member-specific properties
        public List<ReadLoanDto>? Loans { get; set; }  // Loans this member has made
        public List<ReadPenaltyDto>? Penalties { get; set; }  // Penalties for this member
        public bool IsBlocked { get; set; } // Whether the user is blocked (applies to members)
       
        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "The image URL must be a valid .png or .jpg file")]
        public string ImageUrl {set; get;}
    }
}