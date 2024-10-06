using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreatePenaltyDto
    {
        [Required]
        public int LoanId { get; set; }

        [Required, Precision(3, 3)]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; } = false;
    }

}
