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
        public ILoanRepository LoanRepository { get; }
        public IPenaltyRepository PenaltyRepository { get; }
        public IUserRepository UserRepository { get; }
        public IReportRepository ReportRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public UnitOfWork(BookHavenContext context) 
        {
            _context = context;
            BookRepository = new BookRepository(context);
            CategoryRepository = new CategoryRepository(context);
            CategoryRepository = new CategoryRepository(context);
            LoanRepository = new LoanRepository(context);
            PenaltyRepository = new PenaltyRepository(context);  
            UserRepository = new UserRepository(context);   
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
        {
            return new GenericRepository<TEntity>(_context);  /// this will return default implementation, so if you've override the Implementation of one method it won't be affected  
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
