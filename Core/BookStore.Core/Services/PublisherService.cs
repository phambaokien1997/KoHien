using BookStore.Core.Database;
using BookStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Services
{
    public interface IPublisherService
    {
        Task<Dictionary<int, string>> GetNamesAsync();
    }
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _repository;
        public PublisherService(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<int, string>> GetNamesAsync()
        {
            var publishers = _repository.GetAllAsQuery().ToDictionaryAsync(x => x.Id, x => x.Name);

            return await publishers;
        }
    }
}
