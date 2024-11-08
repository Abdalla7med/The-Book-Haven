using Application.DAL;
using Application.DAL.UnitOfWork;
using Application.Shared;
using AutoMapper;
using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    /// no use for AutoMapper  (done)
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
           
            List<ReadPenaltyDto> PenaltyDtos = new List<ReadPenaltyDto>();
           
            foreach (var penalty in penalties) {

                var Dto = new ReadPenaltyDto()
                {
                    PenaltyId = penalty.PenaltyId,
                    Amount = penalty?.Amount ,
                    IsPaid = penalty.IsPaid,
                    LoanId = penalty.LoanId,
                    MemberName = penalty.Member.FirstName
                };

                PenaltyDtos.Add(Dto);
            }
            return PenaltyDtos;
        }

        public async Task<IEnumerable<ReadPenaltyDto>> GetPenaltiesByMember(Guid memberId)
        {
            var penalties = await _unitOfWork.PenaltyRepository.GetPenaltyByMember(memberId);

            List<ReadPenaltyDto> PenaltyDtos = new List<ReadPenaltyDto>();

            foreach (var penalty in penalties)
            {

                var Dto = new ReadPenaltyDto()
                {
                    PenaltyId = penalty.PenaltyId,
                    Amount = penalty?.Amount,
                    IsPaid = penalty.IsPaid,
                    LoanId = penalty.LoanId,
                    MemberName = penalty.Member.FirstName
                };

                PenaltyDtos.Add(Dto);
            }
            return PenaltyDtos;
        }

        public async Task<IEnumerable<ReadPenaltyDto>> GetPaidPenaltiesByMember(Guid MemberId)
        {

            var penalties = await _unitOfWork.PenaltyRepository.GetPenaltyByMember(MemberId);
            // filter it
            penalties = penalties.Where(p => p.IsPaid).ToList();    

            List<ReadPenaltyDto> PenaltyDtos = new List<ReadPenaltyDto>();

            foreach (var penalty in penalties)
            {

                var Dto = new ReadPenaltyDto()
                {
                    PenaltyId = penalty.PenaltyId,
                    Amount = penalty?.Amount,
                    IsPaid = penalty.IsPaid,
                    LoanId = penalty.LoanId,
                    MemberName = penalty.Member.FirstName
                };

                PenaltyDtos.Add(Dto);
            }

            return PenaltyDtos;

        }
        public async Task<ReadPenaltyDto> GetPenaltyByLoan(Guid loanId)
        {
            var penalty = await _unitOfWork.PenaltyRepository.GetPenaltyByLoan(loanId);

            var Dto = new ReadPenaltyDto()
            {
                PenaltyId = penalty.PenaltyId,
                Amount = penalty?.Amount,
                IsPaid = penalty.IsPaid,
                LoanId = penalty.LoanId,
                MemberName = penalty.Member.FirstName
            };

            return Dto;

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

        public async Task<ApplicationResult> PayPenalty(Guid penaltyId, decimal amount)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                var penalty = await _unitOfWork.PenaltyRepository.GetByIdAsync(penaltyId);
                if (penalty == null || penalty.IsPaid)
                   return  new ApplicationResult() { 
                            Succeeded = false, 
                            Errors = new List<string>
                                    {
                                        "No penalty to pay or penalty is already paid." 
                                    } 
                   };

                if (amount < penalty.Amount)
                  return new ApplicationResult() {
                      Succeeded = false,
                      Errors = new List<string>
                                { 
                                    $"Payment is less than the penalty amount. Required: {penalty.Amount:C}" 
                                } 
                  };

                // Mark penalty as paid
                penalty.IsPaid = true;
                await _unitOfWork.PenaltyRepository.UpdateAsync(penalty);
                await _unitOfWork.CompleteAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return new ApplicationResult { Succeeded = true };
            }
        }

        public async Task<ReadPenaltyDto>  GetPenaltyById(Guid PenaltyId)
        {
            var penalty = await _unitOfWork.PenaltyRepository.GetByIdAsync(PenaltyId);

            if (penalty == null)
                throw new ArgumentException("Penalty Not Found");

            var Dto = new ReadPenaltyDto()
            {
                PenaltyId = penalty.PenaltyId,
                Amount = penalty.Amount,
                LoanId = penalty.LoanId,
                MemberName = penalty.Member.FirstName,
                IsPaid = penalty.IsPaid
            };
            return Dto;
        }
    }
}
