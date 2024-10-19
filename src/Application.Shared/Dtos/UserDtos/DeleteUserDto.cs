using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class DeleteUserDto
    {
        [Required]
        public Guid UserId { get; set; }

        public bool HasActiveLoans { get; set; }

        public bool HasUnpaidPenalties { get; set; }
    }
}
