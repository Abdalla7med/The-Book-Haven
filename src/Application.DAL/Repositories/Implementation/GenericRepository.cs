using Application.DAL.Context;
using Application.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class GenericRepository<T>:IRepository<T> where T : class, new()
    {

        protected readonly BookHavenContext _context;
        protected readonly DbSet<T> _dbset;

        public GenericRepository(BookHavenContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity) => await _dbset.AddAsync(entity);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbset.ToListAsync(); /// nav properties null ( lazy loading)

        public virtual async Task<T> GetByIdAsync(Guid id) => await _dbset.FindAsync(id);

        public virtual async Task UpdateAsync(T entity) 
        {
            _dbset.Update(entity);
            await SaveAsync();
        }


        // Soft Delete Method ( no need to declare it as async method ) 
        public async Task  Delete(T entity)
        {
            if (entity is ISoftDeleteable softDeleteableEntity)
            {
                // Mark the entity as deleted instead of physically removing it
                 softDeleteableEntity.IsDeleted = true;
                 await UpdateAsync(entity);  // Mark entity as modified for saving later
            }
            else
            {
                _dbset.Remove(entity); // If not soft deletable, perform actual deletion
            }
        }

        // Soft Delete by Id Method
        public virtual async Task DeleteAsyncById(object id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity != null)
            {
               await Delete(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
