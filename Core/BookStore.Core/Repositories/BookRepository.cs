using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Core.Database;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Core.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> IsExistAsync(IEnumerable<int> bookIds);
    }
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext db) : base(db)
        {
            
        }
        public async Task<bool> IsExistAsync(IEnumerable<int> bookIds)
        {
            var exsitCountBook = await _context.Books.CountAsync(b=> bookIds.Contains(b.Id));
            return exsitCountBook == bookIds.Count();
        }
    }
}
