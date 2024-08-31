using BookStore.Core.Database;
using BookStore.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Services
{
    public interface IBookService
    {
    }
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IAuthorRepository _authorRepository;
        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
           var book = await _repository.GetByIdAsync(id);
            return book;
        }

        public bool AddBookAsync(Book newBook)
        {
            if (!_repository.IsValidEntity(newBook, out List<ValidationResult> errors))
            {
                throw new InvalidOperationException("Data is invalid with these errors: ");
            }
            _repository.AddAsync(newBook);
            
            return true;    
        }
    }
}
