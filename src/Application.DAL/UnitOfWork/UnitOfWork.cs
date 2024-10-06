using Application.DAL.Context;
using Application.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BookHavenContext _context;
        private bool disposedValue = false;

        public IBookRepository BookRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IMemberRepository MemberRepository { get; }
        public ILoanRepository LoanRepository { get; }
        public IPenaltyRepository PenaltyRepository { get; }
        public UnitOfWork(BookHavenContext context) 
        {
            _context = context;
            BookRepository = new BookRepository(context);
            CategoryRepository = new CategoryRepository(context);
            MemberRepository = new MemberRepository(context);
            CategoryRepository = new CategoryRepository(context);
            AuthorRepository = new AuthorRepository(context);
            LoanRepository = new LoanRepository(context);
            PenaltyRepository = new PenaltyRepository(context);     
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
        {
            return new GenericRepository<TEntity>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    /// Free Used Resources
                    _context.Dispose();
                }

                //TODO: free unmanaged resources(unmanaged objects) and override finalizer
                //TODO: set large fields to null
                disposedValue = true;
            }
        }

        /// <summary>
        ///  Defined function inside the IUnitOfWork
        ///  will call the base function 
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true); // Calls the Dispose method to free resources 
            GC.SuppressFinalize(this); // Prevents finalizer from being called
        }

    }
}
