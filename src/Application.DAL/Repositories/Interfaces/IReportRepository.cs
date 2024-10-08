using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<IEnumerable<Report>> GetReportsByEntityAsync(Guid entityId, string entityType);
        Task<IEnumerable<Report>> GetUnresolvedReportsAsync();
    }

}
