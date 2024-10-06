using Application.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DAL
{
    public class Book : ISoftDeleteable
    {
        [Key]
        public int BookId { get; set; }
        [StringLength(25)]
        public string Title { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
        public bool IsDeleted { set; get; } /// Soft Delete Property 
        //  Relationships
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ApplicationUser> Authors { get; set; }

        public ICollection<Loan> Loans { set; get; }


    }
}
