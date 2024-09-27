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
    public interface IAuthorRepository: IRepository<Author>
    {
        Task<List<Author>> GetAllAsync(DateTime bornBefore);
        Task AddAuthorAsync(Author author);
        Task<bool> IsExistAsync(IEnumerable<int> authorIds);
    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookStoreDbContext db) : base(db)
        {
            
        }

        public async Task AddAuthorAsync(Author author)
        {
            if (IsValidEntity(author, out List<ValidationResult> result))
            {
                _dbSet.Add(author);
            }
            else
            {
                // log lỗi
            }
        }

        public async Task<List<Author>> GetAllAsync(DateTime bornBefore)
        {
            var authors = await base.GetAllAsQuery().Where(x => x.DateOfBirth > bornBefore).ToListAsync();

            return authors;
        }

        public async Task<bool> IsExistAsync(IEnumerable<int> authorIds)
        {
            var existCount = await _context.Authors.CountAsync(x => authorIds.Contains(x.Id));
            return existCount == authorIds.Count();
        }

        public bool IsValidEntity(Author entity, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            if (entity.DateOfBirth > DateTime.Now)
            {
                validationResults.Add(new ValidationResult("sai ngay sinh"));
                return false;
            }
            return true;

        }
    }
}
