using Application.DAL.Context;
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
    }
}
