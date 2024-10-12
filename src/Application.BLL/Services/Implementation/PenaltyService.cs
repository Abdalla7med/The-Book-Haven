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
        public Task<IEnumerable<ReadPenaltyDto>> AllPenalties()
        {
            throw new NotImplementedException();
        }

        public Task DeletePenalty(Guid PenaltyId)
        {
            throw new NotImplementedException();
        }

        public Task<ReadPenaltyDto> GetLByPenaltiesByMember(Guid MemberId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPenaltyPaid(Guid PenaltyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePenalty(UpdatePenaltyDto updatePenaltyDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReadPenaltyDto>> GetPenaltiesByMember(Guid memberId)
        {
            var penalties = await _unitOfWork.PenaltyRepository.GetAllAsync();
            penalties = penalties.Where(p => p.MemberId == memberId && !p.IsPaid);

            return _mapper.Map<IEnumerable<ReadPenaltyDto>>(penalties);

        }
        public async Task PayPenalty(Guid loanId, decimal amount)
        {
            var penalty = await _unitOfWork.PenaltyRepository.GetByIdAsync(loanId);

            if (penalty == null || penalty.IsPaid)
                throw new InvalidOperationException("No penalty to pay or penalty is already paid.");

            if (amount < penalty.Amount)
                throw new ArgumentException("Payment is less than the penalty amount.");

            penalty.IsPaid = true;
            await _unitOfWork.PenaltyRepository.UpdateAsync(penalty);
            await _unitOfWork.CompleteAsync();
        }

    }
}
