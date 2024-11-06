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
        /// <summary>
        ///  Adding New Lona into database 
        /// </summary>
        /// <param name="createLoanDto"></param>
        /// <returns></returns>
        Task<ApplicationResult> AddLoan(CreateLoanDto createLoanDto);
        /// <summary>
        ///  Getting all loans in the database 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ReadLoanDto>> AllLoans();
        /// <summary>
        /// Get Loan By Id 
        /// </summary>
        /// <param name="LoanId"></param>
        /// <returns></returns>
        Task<ReadLoanDto> GetLoanById(Guid LoanId);
        Task<IEnumerable<ReadLoanDto>> GetLoansByMember(Guid MemberId);
        Task<IEnumerable<ReadLoanDto>> GetReturnedLoansByMember(Guid MemberId);
        Task<IEnumerable<ReadLoanDto>> GetLoansByBook(Guid BookId);
        /// <summary>
        /// Incase of missing something or edit Due date 
        /// </summary>
        /// <param name="updateLoanDto"></param>
        /// <returns></returns>
        Task UpdateLoan(UpdateLoanDto updateLoanDto);
        /// <summary>
        ///  Deleting Loan Function 
        /// </summary>
        /// <param name="LoanId"></param>
        /// <returns></returns>
        Task DeleteLoan(Guid LoanId);
        /// <summary>
        ///  May be the Same as the UpdateLoan Service, but we need the Update Loan incase of missing some info 
        /// </summary>
        /// <param name="LoanId"></param>
        /// <returns></returns>
        Task ReturnLoan(Guid LoanId);
    }
}
