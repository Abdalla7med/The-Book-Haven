﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Application.DAL.Context
{
    public class BookHavenContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public BookHavenContext() : base() { }

        public BookHavenContext(DbContextOptions<BookHavenContext> options):base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.Id)
            .HasColumnType("uniqueidentifier");

            /// Book - ApplicationUser(Author)	WrittenBy ( many-to-many )	A book can be written by one or more authors.
            modelBuilder.Entity<Book>()
            .HasOne(b => b.Author) // Assuming Authors is a navigation property
            .WithMany(a => a.BooksAuthored);


            /// Book - Category relationship (Many-to-One) BelongsTo relation
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category) // Assuming Category is a navigation property
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId); // Assuming CategoryId is the FK
             ///.OnDelete(DeleteBehavior.Cascade); /// Delete book of its category is been deleted -  as we will do the soft-delete mechanism we'll do validation through BLL


            /// ApplicationUser(Member) - Loan ( Many-to-One)	A member can borrow multiple loans Borrows relation
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(M => M.Loans) // Assuming Loans is a navigation Property
                .WithOne(L => L.Member)
                .HasForeignKey(L => L.MemberId);// Assuming MemberId is the FK
                //.OnDelete(DeleteBehavior.Restrict); /// Prevent deletion of user if has loans -  as we will do the soft-delete mechanism we'll do validation through BLL


            /// Book - Loan relationship (One-to-Many)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book) // Assuming Book is a navigation property
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId); // FK in Loan
                ///.OnDelete(DeleteBehavior.Restrict); as we will do the soft-delete mechanism we'll do validation through BLL


            /// ApplicationUser(Member) - Penalty relationship (One-to-Many)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(m => m.Penalties) // Assuming Penalties is a navigation property
                .WithOne(p => p.Member)
                .HasForeignKey(p => p.MemberId);  // FK in Penalty
                // .OnDelete(DeleteBehavior.Restrict); /// can't delete member with un-paid penalties  -  as we will do the soft-delete mechanism we'll do validation through BLL


            /// Loan - Penalty relationship (One-to-One)	A loan may result in a penalty if the book is returned late.
            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.Loan)
                .WithOne(l => l.Penalty)
                .HasForeignKey<Penalty>(p => p.LoanId);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = Guid.NewGuid(), Name = "Fiction", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Non-Fiction", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Science", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Technology", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "History", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Biography", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Fantasy", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Mystery", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Romance", IsDeleted = false },
                new Category { CategoryId = Guid.NewGuid(), Name = "Self-Help", IsDeleted = false }
            );

            base.OnModelCreating(modelBuilder);
        }


    }
}

