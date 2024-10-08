using Application.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class NotificationRepository:GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(BookHavenContext context) : base(context) { }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId)
        {
            return await _dbset.Where(n => n.UserId == userId)
                               .OrderByDescending(n => n.SentAt)
                               .ToListAsync();
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _dbset.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await SaveAsync(); /// instead of this can user CompleteAsyncin UnitOfWork
            }
        }
    }

}
