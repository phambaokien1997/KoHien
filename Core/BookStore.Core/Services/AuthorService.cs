using BookStore.Core.Repositories;

namespace BookStore.Core.Services
{
    public interface IAuthorService
    {

    }
    public class AuthorService : IAuthorService 
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository repository)
        {
            _authorRepository = repository;
        }
    }
}
