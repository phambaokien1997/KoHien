using BookStore.Core.Database;
using BookStore.Core.Repositories;

namespace BookStore.Core.Services
{
    public interface IAuthorService
    {

    }
    public class AuthorService : IAuthorService 
    {
        private readonly IRepository<Author> _Repository;

        public AuthorService(IRepository<Author> repository)
        {
            _Repository = repository;
        }
    }
}
