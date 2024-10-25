using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task Delete(T entity);
        Task DeleteAsyncById(object id); // for case of GUID
        Task SaveAsync();
        IQueryable<T> GetAllAsQueryable();
    }
}
