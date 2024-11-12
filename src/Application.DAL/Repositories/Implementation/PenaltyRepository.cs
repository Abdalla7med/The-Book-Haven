using Application.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class PenaltyRepository:GenericRepository<Penalty>, IPenaltyRepository
    {
        public PenaltyRepository(BookHavenContext _context) :base(_context) { }

        public override async Task<IEnumerable<Penalty>> GetAllAsync()
        {
            return await _dbset.Include(P => P.Member)
                                .Include(P => P.Loan)
                                .ToListAsync();
        }
        public override async Task<Penalty> GetByIdAsync(Guid id)
        {
            return await _dbset.Include(P => P.Member)
                               .Include(P=>P.Loan)
                               .FirstOrDefaultAsync(P => P.PenaltyId == id && !P.IsPaid);
        }

        public async Task<IEnumerable<Penalty>> GetPenaltyByMember(Guid MemberId)
        {
            return await _dbset.Include(p => p.Member)
                                .Include(p => p.Loan)
                                .Where(p => p.MemberId == MemberId)
                                .ToListAsync();
        }

        public async Task<Penalty> GetPenaltyByLoan(Guid LoanId)
        {
           return await _dbset.FirstOrDefaultAsync(P => P.LoanId == LoanId && !P.IsPaid);
        }


    }
}
