using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ReadBookDto
    {
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
        public string CategoryName { get; set; } // this will be a logic layer handled
        public List<string> AuthorNames { get; set; }
        public bool IsDeleted { get; set; }

    }

}
