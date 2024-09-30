using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Application.DAL.Context
{
    public class BookHavenContext : DbContext
    {

        public BookHavenContext() : base() { }

        public BookHavenContext(DbContextOptions<BookHavenContext> options):base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
   
            //Book - Author	WrittenBy	A book can be written by one or more authors.
            modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors) // Assuming Authors is a navigation property
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("BookAuthors")); // Branch table


            // Book - Category relationship (Many-to-One) BelongsTo relation
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category) // Assuming Category is a navigation property
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId); // Assuming CategoryId is the FK


            /// Member - Loan ( Many-to-One)	A member can borrow multiple loans Borrows relation
            modelBuilder.Entity<Member>()
                .HasMany(M => M.Loans) // Assuming Loans is a navigation Property
                .WithOne(L => L.Member)
                .HasForeignKey(L => L.MemberId); // Assuming MemberId is the FK


            /// Book - Loan relationship (One-to-Many)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book) // Assuming Book is a navigation property
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId); // FK in Loan


            /// Member - Penalty relationship (One-to-Many)
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Penalties) // Assuming Penalties is a navigation property
                .WithOne(p => p.Member)
                .HasForeignKey(p => p.MemberId); // FK in Penalty


            /// Loan - Penalty relationship (One-to-One)	A loan may result in a penalty if the book is returned late.
            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.Loan)
                .WithOne(l => l.Penalty)
                .HasForeignKey<Penalty>(p => p.LoanId);


            base.OnModelCreating(modelBuilder);
        }


    }
}

