using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class UpdateBookDto
    {
        [Required]
        public Guid BookId { get; set; }

        public int AvailableCopies { get; set; }


    }

}
