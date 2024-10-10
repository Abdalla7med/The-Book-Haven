using Application.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Notification : ISoftDeleteable
    {
        public Guid NotificationId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }  // Foreign Key to ApplicationUser GUID
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; } = false;  // Indicates if the notification has been read
        public bool IsDeleted { set; get; } = false;
        public ApplicationUser User { get; set; }  // Navigation Property
    }

}
