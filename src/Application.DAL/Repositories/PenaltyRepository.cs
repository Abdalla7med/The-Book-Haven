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

        public override async Task<Penalty> GetByIdAsync(int id)
        {
            return await _dbset.Include(P => P.Member)
                               .FirstOrDefaultAsync(P => P.PenaltyId == id);
        }
    }
}
