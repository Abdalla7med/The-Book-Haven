using Application.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
       BookRepository BookRepository { get; }
       CategoryRepository CategoryRepository { get; }
       LoanRepository LoanRepository { get; }
       PenaltyRepository PenaltyRepository { get; }
       UserRepository UserRepository { get; }
       //IReportRepository ReportRepository { get; }
        
       //INotificationRepository NotificationRepository { get; }
        Task<int> CompleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        ///  Follow Reflection 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> Repository<T>() where T : class, new(); 

    }
}
