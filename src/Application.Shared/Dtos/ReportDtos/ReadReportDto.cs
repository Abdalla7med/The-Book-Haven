using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ReadReportDto
    {
        public int ReportId { get; set; }
        public string ReportedById { get; set; }
        public string ReportedEntityId { get; set; }
        public string ReportedByName { get; set; }
        public string Description { get; set; }
        public string EntityType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsResolved { get; set; }
    }

}
