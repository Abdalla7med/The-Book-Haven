using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface IPenaltyRepository:IRepository<Penalty>
    {

        Task<IEnumerable<Penalty>> GetPenaltyByMember(Guid MemberId);
        Task<Penalty> GetPenaltyByLoan(Guid LoanId);
    }
}
