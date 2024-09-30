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


            base.OnModelCreating(modelBuilder);
        }


    }
}

