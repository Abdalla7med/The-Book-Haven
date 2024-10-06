using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreateBookDto
    {
        [Required, StringLength(25)]
        public string Title { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public int AvailableCopies { get; set; }

        [Required]
        public int CategoryId { get; set; }  // Select category

        [Required]
        public List<string> AuthorNames { get; set; }  // Add author names manually
    }

}
