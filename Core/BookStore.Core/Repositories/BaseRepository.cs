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
        bool IsValidEntity(T entity, out List<ValidationResult> validationResults);
        Task SaveChangesAsync();
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
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1, int pageSize = 20)
        {
            //return await _dbSet.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
            return await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IQueryable<T> GetAllAsQuery(int pageIndex = 1, int pageSize = 10)
        {
            //return _dbSet.Skip(pageSize * pageIndex).Take(pageSize);
            return _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (log, throw ra ngoài, v.v.)
                throw new InvalidOperationException("Error adding entity", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(); // Lưu thay đổi ngay lập tức
        }
        public async Task DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(); // Lưu thay đổi ngay lập tức
        }
        public bool IsValidEntity(T entity, out List<ValidationResult> validationResults)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(entity, context, validationResults, true);
            return isValid;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
