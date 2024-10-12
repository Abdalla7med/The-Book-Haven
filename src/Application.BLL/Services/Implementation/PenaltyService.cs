using Application.DAL;
using Application.DAL.UnitOfWork;
using Application.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PenaltyService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReadPenaltyDto>> AllPenalties()
        {
            var penalties = await _unitOfWork.PenaltyRepository.GetAllAsync();
            // Filtering
            penalties = penalties.Where(p => !p.IsPaid && !p.IsDeleted);
            return _mapper.Map<IEnumerable<ReadPenaltyDto>>(penalties);
        }

        public async Task<IEnumerable<ReadPenaltyDto>> GetPenaltiesByMember(Guid memberId)
        {
            var penalties = await _unitOfWork.PenaltyRepository.GetPenaltyByMember(memberId);
            return _mapper.Map<IEnumerable<ReadPenaltyDto>>(penalties);
        }

        public async Task<ReadPenaltyDto> GetPenaltyByLoan(Guid loanId)
        {
            var penalty = await _unitOfWork.PenaltyRepository.GetPenaltyByLoan(loanId);
            return _mapper.Map<ReadPenaltyDto>(penalty);
        }

        public async Task AddPenalty(CreatePenaltyDto createPenaltyDto)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(createPenaltyDto.MemberId);
                if (user == null)
                    throw new ArgumentException("User doesn't exist");

                var loan = await _unitOfWork.LoanRepository.GetByIdAsync(createPenaltyDto.LoanId);
                if (loan == null)
                    throw new ArgumentException("There's no loan corresponding to this penalty");

                // Mapping step
                Penalty penalty = new Penalty
                {
                    Amount = createPenaltyDto.Amount,
                    Member = user,
                    MemberId = user.Id,
                    Loan = loan,
                    LoanId = loan.LoanId,
                    IsPaid = false
                };

                await _unitOfWork.PenaltyRepository.AddAsync(penalty);
                await _unitOfWork.CompleteAsync();

                // Commit transaction if everything is successful
                await transaction.CommitAsync();
            }
        }

        public async Task PayPenalty(Guid penaltyId, decimal amount)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                var penalty = await _unitOfWork.PenaltyRepository.GetByIdAsync(penaltyId);
                if (penalty == null || penalty.IsPaid)
                    throw new InvalidOperationException("No penalty to pay or penalty is already paid.");

                if (amount < penalty.Amount)
                    throw new ArgumentException($"Payment is less than the penalty amount. Required: {penalty.Amount:C}");

                // Mark penalty as paid
                penalty.IsPaid = true;
                await _unitOfWork.PenaltyRepository.UpdateAsync(penalty);
                await _unitOfWork.CompleteAsync();

                // Commit the transaction
                await transaction.CommitAsync();
            }
        }
    }


    //public Task DeletePenalty(Guid PenaltyId)
    //{
    //    throw new NotImplementedException();
    //}


    //public Task<bool> IsPenaltyPaid(Guid PenaltyId)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task UpdatePenalty(UpdatePenaltyDto updatePenaltyDto)
    //{
    //    throw new NotImplementedException();
    //}


}
