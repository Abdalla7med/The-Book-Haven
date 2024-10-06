using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ReadUserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        // Roles
        public List<string> Roles { get; set; }

        // Author-specific properties
        public List<CreateBookDto> BooksAuthored { get; set; }  // Books authored by this user

        // Member-specific properties
        public List<CreateLoanDto> Loans { get; set; }  // Loans this member has made
        public List<CreatePenaltyDto> Penalties { get; set; }  // Penalties for this member

        public bool IsBlocked { get; set; } // Whether the user is blocked (applies to members)
    }

}
