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
        public int CategoryId { get; set; }

        [StringLength(25)]
        public string Name { get; set; }
        public bool IsDeleted { set; get; }
        // relations
        public ICollection<Book>? Books { get; set; }
    }
}
