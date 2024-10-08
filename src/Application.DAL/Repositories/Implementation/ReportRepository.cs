using Application.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(BookHavenContext context) : base(context) { }

        public async Task<IEnumerable<Report>> GetReportsByEntityAsync(Guid entityId, string entityType)
        {
            return await _dbset.Where(r => r.ReportedEntityId == entityId && r.EntityType == entityType)
                               .OrderByDescending(r => r.CreatedAt)
                               .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetUnresolvedReportsAsync()
        {
            return await _dbset.Where(r => !r.IsResolved)
                               .OrderBy(r => r.CreatedAt)
                               .ToListAsync();
        }
    }

}
