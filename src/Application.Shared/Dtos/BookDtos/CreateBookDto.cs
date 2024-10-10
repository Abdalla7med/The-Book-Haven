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
        public Guid CategoryId { get; set; }  // Select category

        [Required]
        public List<string> AuthorNames { get; set; }  // Add author names manually
    }

}


/*
 using Application.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DAL
{
    public class Book : ISoftDeleteable
    {
        [Key]
        public Guid BookId { get; set; } = Guid.NewGuid(); // auto-generated 
        [StringLength(25)]
        public string Title { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
        public bool IsDeleted { set; get; } = false; /// Soft Delete Property 
        //  Relationships
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; } // nav property associated with category 

        public ICollection<ApplicationUser> Authors { get; set; }

        public ICollection<Loan> Loans { set; get; } // one - many mapping of relation btw book and laon 


    }
}

 */