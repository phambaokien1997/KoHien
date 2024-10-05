using BookStore.Core.Database;
using BookStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Services
{
    public interface IGenreService
    {
        Task<Dictionary<int, string>> GetNamesAsync();
    }
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;
        public GenreService(IRepository<Genre> repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<int, string>> GetNamesAsync()
        {
            var genres = _repository.GetAllAsQuery().ToDictionaryAsync(x => x.Id, x => x.Name);

            return await genres;
        }
    }
}
