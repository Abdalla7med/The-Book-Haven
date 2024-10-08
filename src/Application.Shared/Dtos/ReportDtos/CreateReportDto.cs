using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreateReportDto
    {
        public string ReportedById { get; set; }
        public string ReportedEntityId { get; set; }
        public string Description { get; set; }
        public string EntityType { get; set; }  // Could be "Book", "User", etc.
    }
}

