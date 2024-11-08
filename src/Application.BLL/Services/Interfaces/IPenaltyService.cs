using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public interface IPenaltyService
    {
        Task<IEnumerable<ReadPenaltyDto>> AllPenalties();
        Task<ReadPenaltyDto> GetPenaltyByLoan(Guid LoanId);
        Task<IEnumerable<ReadPenaltyDto>> GetPenaltiesByMember(Guid MemberId);
        Task<IEnumerable<ReadPenaltyDto>> GetPaidPenaltiesByMember(Guid MemberId);
        Task<ApplicationResult> PayPenalty(Guid loanId, decimal amount);
        Task AddPenalty(CreatePenaltyDto createPenaltyDto);

        Task<ReadPenaltyDto> GetPenaltyById(Guid PenaltyId);


    }
}
