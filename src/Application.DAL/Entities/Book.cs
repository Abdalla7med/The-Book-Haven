using Application.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DAL
{
    public class Book : ISoftDeleteable
    {
        [Key]
        public Guid BookId { get; set; } = Guid.NewGuid(); // auto-generated 
        [StringLength(25)]
        public string? Title { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? CoverUrl { get; set; }
        public string? ISBN { get; set; }
        public int PublicationYear { get; set; } = DateTime.UtcNow.Year;
        public int? AvailableCopies { get; set; } = 10;
        public bool IsDeleted { set; get; } = false; /// Soft Delete Property 
        //  Relationships
        public Guid? CategoryId { get; set; } 
        public Category? Category { get; set; } // nav property associated with category 

        public Guid? AuthorId { get; set; }
        public ApplicationUser? Author { get; set; }

        public ICollection<Loan>? Loans { set; get; } // one - many mapping of relation btw book and loan 


    }
}
