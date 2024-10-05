using BookStore.Core.Database;
using BookStore.Core.Models;
using BookStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BookStore.Core.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorItem>> GetAuthorsAsync();
        Task<AuthorItem?> GetByIdAsync(int id);
        Task<AuthorItem?> CreateAuthorAsync(AuthorItem input);
        Task<AuthorItem?> DeleteAuthorAsync(int id);
        Task<AuthorItem?> UpdateAuthorAsync(Author updatedAuthor);
        Task<Dictionary<int,string>> GetNamesAsync();
    }
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        private readonly IBookRepository _bookRepository;

        public AuthorService(IRepository<Author> repository, IBookRepository bookRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }
        private AuthorItem ConvertToAuthorItem(Author author)
        {
            return new AuthorItem
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth.ToString("yyyy-MM-dd"),
                Gender = author.Gender,
                Country = author.Country,
                PhoneNumber = author.PhoneNumber,
                Books = author.BookAuthors.Select(book => book.Book.Name).ToArray(),
                BookIds = author.BookAuthors.Select(book => book.BookId).ToArray(),
                Genres = author.AuthorGenres.Select(genre => genre.Genre.Name).ToArray(),
                GenreIds = author.AuthorGenres.Select(genre => genre.GenreId).ToArray(),
            };
        }
        public async Task<List<AuthorItem>> GetAuthorsAsync()
        {
            var authors = await _repository.GetAllAsQuery()
                .Include(a => a.AuthorGenres)
                .ThenInclude(ag => ag.Genre)
                .Include(a => a.BookAuthors)
                .ThenInclude(ab => ab.Book)
                .ToListAsync();
            return authors.Select(ConvertToAuthorItem).ToList();
        }
        public async Task<AuthorItem?> GetByIdAsync(int id)
        {
            var author = await _repository.GetAllAsQuery()
                .Include(a => a.AuthorGenres)
                .ThenInclude(ag => ag.Genre)
                .Include(a => a.BookAuthors)
                .ThenInclude(ab => ab.Book)
                .SingleOrDefaultAsync(a => a.Id == id);
            if(author == null)
            {
                return null;
            }
            var authorItem = ConvertToAuthorItem(author);
            return authorItem;
        }
        public async Task<AuthorItem?> CreateAuthorAsync(AuthorItem input)
        {
            //Kiểm tra tính hợp lệ của sách
            var isBookValid = await _bookRepository.IsExistAsync(input.BookIds);
            if (!isBookValid)
            {
                throw new Exception($"Invalid Books");
            }
            var books = input.BookIds.Select(i => new BookAuthor
            {
                BookId = i,
            }).ToList();
            var genres = input.GenreIds.Select(i => new AuthorGenre
            {
                GenreId = i,
            }).ToList();
            var newAuthor = new Author
            {
                Name = input.Name,
                DateOfBirth = DateTime.Parse(input.DateOfBirth),
                Gender = input.Gender,
                Country = input.Country,
                PhoneNumber = input.PhoneNumber,
                BookAuthors = books,
                AuthorGenres = genres,
            };
            if (!_repository.IsValidEntity(newAuthor, out List<ValidationResult> errors))
            {
                var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException($"Data is invalid with these errors: {errorMessage}");
            }
            await _repository.AddAsync(newAuthor);
            var authorItem = await GetByIdAsync(newAuthor.Id);
            return authorItem;
        }
        public async Task<AuthorItem?> DeleteAuthorAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
                return null;
            }
            await _repository.DeleteAsync(author);
            var authorItem = ConvertToAuthorItem(author);
            return authorItem;
        }
        public async Task<AuthorItem?> UpdateAuthorAsync(Author updatedAuthor)
        {
            if (!_repository.IsValidEntity(updatedAuthor, out List<ValidationResult> errors))
            {
                var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException("Data is invalid with these errors" + errorMessage);
            }
            var existingAuthor = await _repository.GetByIdAsync(updatedAuthor.Id);
            if(existingAuthor == null)
            {
                return null;
            }
            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.PhoneNumber = updatedAuthor.PhoneNumber;
            existingAuthor.DateOfBirth = updatedAuthor.DateOfBirth;
            existingAuthor.Country = updatedAuthor.Country;
            existingAuthor.Gender = updatedAuthor.Gender;
            existingAuthor.AuthorGenres = updatedAuthor.AuthorGenres;
            existingAuthor.BookAuthors= updatedAuthor.BookAuthors;
            await _repository.UpdateAsync(existingAuthor);
            var updatedAuthorItem = ConvertToAuthorItem(updatedAuthor);
            return updatedAuthorItem;
        }

        public async Task<Dictionary<int, string>> GetNamesAsync()
        {
            var authors = _repository.GetAllAsQuery().ToDictionaryAsync(x => x.Id, x => x.Name);

            return await authors;
        }
    }
}
