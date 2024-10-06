using Application.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Author : ISoftDeletable
    {
        public int AuthorId { get; set; }
        [StringLength(50)] // represents the full name 
        public string Name { get; set; }
        public bool IsBlocked { set; get; }
        public bool IsDeleted { set; get; }
        public ICollection<Book> Books { get; set; }
    }
}
