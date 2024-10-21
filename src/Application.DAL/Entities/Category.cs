using Application.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Category : ISoftDeleteable
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        [StringLength(25)]
        public string? Name { get; set; }
        public bool IsDeleted { set; get; } = false;
        // relations
        public ICollection<Book>? Books { get; set; }
    }
}
