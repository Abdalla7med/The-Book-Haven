using Application.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class LoanRepository: GenericRepository<Loan>, ILoanRepository
    {

        public LoanRepository(BookHavenContext _context) : base(_context) { }

        public override async Task<Loan> GetByIdAsync(int id)
        {
            return await _dbset.Include(l => l.Penalty)        // Include Penalty
                               .ThenInclude(p => p.Member)     // Include related Member from Penalty
                               .Include(l => l.Book)           // Include related Book
                               .FirstOrDefaultAsync(l => l.LoanId == id);  // Get the specific loan by ID
        }

    }
}
