﻿using Application.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
       IBookRepository BookRepository { get; }
       ICategoryRepository CategoryRepository { get; }
       ILoanRepository LoanRepository { get; }
       IPenaltyRepository PenaltyRepository { get; }
       IUserRepository UserRepository { get; }
       IReportRepository ReportRepository { get; }
       INotificationRepository NotificationRepository { get; }

       Task<int> CompleteAsync();

        IRepository<T> Repository<T>() where T : class, new(); 

    }
}
