using Application.DAL;
using Application.DAL.UnitOfWork;
using Application.Shared;
using AutoMapper;

namespace Application.BLL
{
    /// No use of AutoMapper (done) 
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReadLoanDto> GetLoanById(Guid loanId)
        {
            var loan = await _unitOfWork.LoanRepository.GetByIdAsync(loanId);

            if (loan == null)
                throw new ArgumentException("Loan not found");
            var Dto = new ReadLoanDto()
            {
                LoanId = loan.LoanId,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                IsReturned = loan.IsReturned,
                BookTitle = loan.Book.Title,
                MemberName = loan.Member.FirstName, 
            };
            return Dto;
        }

        public async Task<IEnumerable<ReadLoanDto>> AllLoans()
        {
            var loans = await _unitOfWork.LoanRepository.GetAllAsync();
            List<ReadLoanDto> loanDtos = new List<ReadLoanDto>();

            foreach(var loan in loans)
            {
                var Dto = new ReadLoanDto()
                {
                    LoanId = loan.LoanId,
                    LoanDate = loan.LoanDate,
                    DueDate = loan.DueDate,
                    ReturnDate = loan.ReturnDate,
                    IsReturned = loan.IsReturned,
                    BookTitle = loan.Book.Title,
                    MemberName = loan.Member.FirstName,
                };

                loanDtos.Add(Dto);
            }

            return loanDtos;
        }

        public async Task<IEnumerable<ReadLoanDto>> GetLoansByMember(Guid memberId)
        {
            var loans = await _unitOfWork.LoanRepository.GetAllAsync();
            loans = loans.Where(l => l.MemberId == memberId).ToList();

            List<ReadLoanDto> loanDtos = new List<ReadLoanDto>();

            foreach (var loan in loans)
            {
                var Dto = new ReadLoanDto()
                {
                    LoanId = loan.LoanId,
                    LoanDate = loan.LoanDate,
                    DueDate = loan.DueDate,
                    ReturnDate = loan.ReturnDate,
                    IsReturned = loan.IsReturned,
                    BookTitle = loan.Book.Title,
                    MemberName = loan.Member.FirstName,
                };

                loanDtos.Add(Dto);
            }

            return loanDtos;
        }

        public async Task<IEnumerable<ReadLoanDto>> GetReturnedLoansByMember(Guid MemberId)
        {

            var loans = await _unitOfWork.LoanRepository.GetAllAsync();
            loans = loans.Where(l => l.IsReturned); /// Get Returned Loans

            List<ReadLoanDto> loanDtos = new List<ReadLoanDto>();

            foreach (var loan in loans)
            {
                var Dto = new ReadLoanDto()
                {
                    LoanId = loan.LoanId,
                    LoanDate = loan.LoanDate,
                    DueDate = loan.DueDate,
                    ReturnDate = loan.ReturnDate,
                    IsReturned = loan.IsReturned,
                    BookTitle = loan.Book.Title,
                    MemberName = loan.Member.FirstName,
                };

                loanDtos.Add(Dto);
            }

            return loanDtos;

        }
        public async Task<IEnumerable<ReadLoanDto>> GetLoansByBook(Guid bookId)
        {
            var loans = await _unitOfWork.LoanRepository.GetAllAsync();
            loans = loans.Where(l => l.BookId == bookId);

            List<ReadLoanDto> loanDtos = new List<ReadLoanDto>();

            foreach (var loan in loans)
            {
                var Dto = new ReadLoanDto()
                {
                    LoanId = loan.LoanId,
                    LoanDate = loan.LoanDate,
                    DueDate = loan.DueDate,
                    ReturnDate = loan.ReturnDate,
                    IsReturned = loan.IsReturned,
                    BookTitle = loan.Book.Title,
                    MemberName = loan.Member.FirstName,
                };

                loanDtos.Add(Dto);
            }

            return loanDtos;
        }

        public async Task<ApplicationResult> AddLoan(CreateLoanDto createLoanDto)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                // Fetch user and book
                var user = await _unitOfWork.UserRepository.GetByIdAsync(createLoanDto.MemberId);
                if (user == null)
                {
                    return new ApplicationResult
                    {
                        Succeeded = false,
                        Errors = new List<string> { "Member doesn't exist." }
                    };
                }

                var book = await _unitOfWork.BookRepository.GetByIdAsync(createLoanDto.BookId);
                if (book == null || book.IsDeleted || book.AvailableCopies < 1)
                {
                    return new ApplicationResult
                    {
                        Succeeded = false,
                        Errors = new List<string> { "Book not available." }
                    };
                }

                // Validate or calculate DueDate
                DateTime dueDate = createLoanDto.DueDate != DateTime.MinValue
                    ? createLoanDto.DueDate
                    : DateTime.UtcNow.AddDays(14); // Default loan period, if needed

                // Create loan entity
                var loan = new Loan
                {
                    BookId = book.BookId,
                    MemberId = user.Id,
                    DueDate = dueDate,
                    LoanDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsReturned = false
                };

                // Decrement available copies and update
                book.AvailableCopies -= 1;
                await _unitOfWork.BookRepository.UpdateAsync(book);

                // Add loan
                await _unitOfWork.LoanRepository.AddAsync(loan);

                // Save changes
                await _unitOfWork.CompleteAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return new ApplicationResult { Succeeded = true };
            }

            /// there's not a valid place to add return statement 
        }

        public async Task ReturnLoan(Guid loanId)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                var loan = await _unitOfWork.LoanRepository.GetByIdAsync(loanId);
                if (loan == null || loan.IsDeleted)
                    throw new ArgumentException("Loan doesn't exist");

                if (loan.IsReturned)
                    throw new InvalidOperationException("Loan has already been returned");

              
                if (loan.DueDate < DateTime.UtcNow)
                {
                    // Calculate penalty
                    var penaltyAmount = (decimal)((DateTime.UtcNow - loan.DueDate).TotalDays * 1.5);
                    var penalty = new Penalty
                    {
                        Loan = loan,
                        LoanId = loanId,
                        Member = loan.Member,
                        MemberId = loan.MemberId,
                        Amount = penaltyAmount
                    };

                    // Add penalty
                    await _unitOfWork.PenaltyRepository.AddAsync(penalty);
                    await _unitOfWork.CompleteAsync();

                    // Throw to let middleware handle it if penalty is due
                    throw new InvalidOperationException($"Please pay the penalty of {penaltyAmount:C} before returning this loan.");
                }

                // Mark loan as returned
                loan.IsReturned = true;

                // Increase available copies
                if (loan.Book != null)
                {
                    loan.Book.AvailableCopies += 1;
                    await _unitOfWork.BookRepository.UpdateAsync(loan.Book);
                }

                // Update loan and save changes
                await _unitOfWork.LoanRepository.UpdateAsync(loan);
                await _unitOfWork.CompleteAsync();

                // Commit transaction
                await transaction.CommitAsync();
            }
        }



        public async Task UpdateLoan(UpdateLoanDto updateLoanDto)
        {
            /// Check for Existence of Loan  
            var Loan = await _unitOfWork.LoanRepository.GetByIdAsync(updateLoanDto.LoanId);
            /// the reason for not checking the IsReturned, thus because IsReturned may be the reason for update 
            if (Loan == null || Loan.IsDeleted)
                throw new ArgumentException("Loan Doesn't Exists");

            /// Can't Mark loan as returned before the due date 
            if (updateLoanDto.IsReturned && Loan.DueDate > DateTime.UtcNow)
                throw new InvalidOperationException("Cannot mark loan as returned before the due date.");

            Loan.IsReturned = updateLoanDto.IsReturned;
            Loan.IsDeleted = updateLoanDto.IsDeleted;
            Loan.ReturnDate = updateLoanDto.ReturnDate;
            Loan.DueDate = updateLoanDto.DueTime;

            await _unitOfWork.LoanRepository.UpdateAsync(Loan);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteLoan(Guid LoanId)
        {
            /// Getting Loan using Id
            var Loan = await _unitOfWork.LoanRepository.GetByIdAsync(LoanId);

            /// Check for existence of loan
            if (Loan == null || Loan.IsDeleted)
                throw new ArgumentException("Loan Doesn't Exists");

            /// Check if Loan IsReturned or Not 
            if (!Loan.IsReturned || Loan.Penalty?.IsPaid == false)
                throw new InvalidOperationException("Action Can't be done");

            /// Updating the IsDeleted Property 
            Loan.IsDeleted = true;

            /// Updating Loan in the table 
            await _unitOfWork.LoanRepository.UpdateAsync(Loan);
            await _unitOfWork.CompleteAsync();
        }
    }
}
