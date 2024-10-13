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
        [Required]
        [MaxLength(50, ErrorMessage = "Title must be less than 50 characters")]
        [MinLength(5, ErrorMessage = "Title must be greater than 4 characters")]
        public string Title { get; set; }

        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "The image URL must be a valid .png or .jpg file")]
        public string CoverUrl { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public int AvailableCopies { get; set; }

        [Required]
        public Guid CategoryId { get; set; }  // Select category

        [Required]
        public List<string> AuthorNames { get; set; }  // Add author names manually
    }

}