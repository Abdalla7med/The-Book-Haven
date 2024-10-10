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

        [StringLength(25)]
        public string Title { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }

        public Guid CategoryId { get; set; }  // Update category if necessary

        public List<string> AuthorNames { get; set; }  // Update authors

        // Admin controls the soft-delete feature
        public bool IsDeleted { get; set; }  // Admin-specific field for soft delete
    }

}
