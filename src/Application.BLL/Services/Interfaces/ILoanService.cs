using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public interface ILoanService
    {
        Task AddLoan(CreateLoanDto createLoanDto);
        Task<IEnumerable<ReadLoanDto>> AllLoans();
        Task<ReadLoanDto> GetBookById(Guid LoanId);
        Task UpdateLoan(UpdateLoanDto updateLoanDto);
        Task DeleteLoan(Guid LoanId);
        Task ReturnLoan(Guid LoanId);
    }
}
