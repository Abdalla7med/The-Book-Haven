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
    public class LoanService : ILoanService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task AddLoan(CreateLoanDto createLoanDto)
        {
            ///// check book exists -> book reposiotry
            ///// user -> userrepos
            ///// loan -> loan 
            //var bookid = Guid.NewGuid();
            //var book = await _unitOfWork.BookRepository.GetByIdAsync(bookid); 
            //if(book != null)
            //{
            //    var user = await _unitOfWork.UserRepository.GetByIdAsync();
            //    if (user != null)
            //    {
            //        await _unitOfWork.LoanRepository.AddAsync(_mapper.Map<Loan>(createLoanDto));
                    
            //        book.AvailableCopies -= 1;
            //        await _unitOfWork.BookRepository.UpdateAsync(book);
            //        await _unitOfWork.CompleteAsync();

            //    }
            //}
            
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadLoanDto>> AllLoans()
        {
            throw new NotImplementedException();
        }

        public Task DeleteLoan(Guid LoanId)
        {
            throw new NotImplementedException();
        }

        public Task<ReadLoanDto> GetBookById(Guid LoanId)
        {
            throw new NotImplementedException();
        }

        public Task ReturnLoan(Guid LoanId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLoan(UpdateLoanDto updateLoanDto)
        {
            throw new NotImplementedException();
        }
    }
}
