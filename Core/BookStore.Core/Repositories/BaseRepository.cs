using BookStore.Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1, int pageSize = 10);
        IQueryable<T> GetAllAsQuery(int pageIndex = 1, int pageSize = 20);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        bool IsValidEntity<T>(T entity, out List<ValidationResult> validationResults);
    }
    public class BaseRepository<T> : IRepository<T>  where T : BaseEntity
    {
        protected readonly BookStoreDbContext _context;

        protected readonly DbSet<T> _dbSet;
        public BaseRepository(BookStoreDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _context = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1, int pageSize = 20)
        {
            return await _dbSet.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
        }

        public IQueryable<T> GetAllAsQuery(int pageIndex = 1, int pageSize = 10)
        {
            return _dbSet.Skip(pageSize * pageIndex).Take(pageSize);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask; // No async operation here, so returning a completed task.
        }

        public async Task DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            await Task.CompletedTask; // No async operation here, so returning a completed task.
        }
        public bool IsValidEntity<T>(T entity, out List<ValidationResult> validationResults)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(entity, context, validationResults, true);
            return isValid;
        }
    }
}
