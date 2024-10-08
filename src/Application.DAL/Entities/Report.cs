using Application.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Report : ISoftDeleteable
    {
        public Guid ReportId { get; set; } = Guid.NewGuid();
        public Guid ReportedById { get; set; }  // Foreign Key to ApplicationUser
        public Guid ReportedEntityId { get; set; }  // Could be a BookId, UserId, etc.
        public string Description { get; set; }
        public string EntityType { get; set; }  // "Book", "User", etc.
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsResolved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public ApplicationUser ReportUser { get; set; }  // Navigation Property
    }

}
