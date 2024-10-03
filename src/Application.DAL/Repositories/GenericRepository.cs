using Application.DAL.Context;
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

        private readonly BookHavenContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(BookHavenContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbset.AddAsync(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbset.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbset.FindAsync(id);

        public async Task UpdateAsync(T entity) => _dbset.Update(entity);

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) _dbset.Remove(entity);
        }
    }
}
