using BookStore.Core.Database;
using BookStore.Core.Models;
using BookStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Services
{
    public interface IBookService
    {
        Task<List<BookItem>> GetBooksAsync();
        Task<BookItem?> GetByIdAsync(int id);
        Task<BookItem?> CreateBookAsync(BookItem newBook);
        Task<BookItem?> DeleteBookAsync(int id);
        Task<BookItem?> UpdateBookAsync(Book updatedBook);
    }
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IAuthorRepository _authorRepository;
        public BookService(IRepository<Book> repository, IAuthorRepository authorRepository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
        }
        private BookItem ConvertToBookItem(Book book)
        {
            return new BookItem
            {
                Id = book.Id,
                Title = book.Title,
                Name = book.Name,
                ShortDescription = book.ShortDescription,
                Price = book.Price,
                Quantity = book.Quantity,
                PublicationDate = book.PublicationDate.ToString("yyyy-MM-dd"),
                GenreId = book.GenreId,
                Genre = book.Genre?.Name,
                PublisherId = book.PublisherId,
                Publisher = book.Publisher?.Name,
                Authors = book.BookAuthors.Select(author => author.Author.Name).ToArray(),
                AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToArray(),
                CreatedAt = book.CreatedAt.ToString("yyyyy-MM-dd")
            };
        }

        public async Task<BookItem?> GetByIdAsync(int id)
        {
            var book = await _repository.GetAllAsQuery()
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return null;
            }
            var bookItem = ConvertToBookItem(book);
            return bookItem;
            //return book != null ? ConvertToBookItem(book) : null;
        }

        public async Task<List<BookItem>> GetBooksAsync()
        {
            //var bookQuery =  _repository.GetAllAsQuery();
            //lazy loading
            //var a = bookQuery.Select(book => new BookItem
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    Name = book.Name,
            //    ShortDescription = book.ShortDescription,
            //    Price = book.Price,
            //    Quantity = book.Quantity,
            //    PublicationDate = book.PublicationDate.ToString("yyyy-MM-dd"),
            //    GenreId = book.GenreId,
            //    Genre = book.Genre.Name,
            //    PublisherId = book.PublisherId,
            //    Publisher = book.Publisher.Name,
            //    Authors = book.Authors.Select(author => author.Author.Name).ToArray()
            //});
            //var resultLazy = await a.ToListAsync();

            //eager loading
            //bookQuery = bookQuery.Include(x => x.Authors).Include(x => x.Genre).Include(x => x.Publisher);
            //var books = await bookQuery.ToListAsync();
            //var edgerResult = books.Select(ConvertToBookItem).ToList();

            //return resultLazy;
            var books = await _repository.GetAllAsQuery()
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)// Eager load authors
                .Include(b => b.Genre)    // Eager load genre
                .Include(b => b.Publisher) // Eager load publisher
                .ToListAsync();

            return books.Select(ConvertToBookItem).ToList();

        }

        public async Task<BookItem?> CreateBookAsync(BookItem input)
        {
            //validate authors
            var isAuthorValid = await _authorRepository.IsExistAsync(input.AuthorIds);
            if (!isAuthorValid)
            {
                throw new Exception($"Invalid Authors");
            }
            var authors = input.AuthorIds.Select(x => new BookAuthor
            {
                AuthorId = x,
            }).ToList();
            var newBook = new Book
            {
                ShortDescription = input.ShortDescription ?? "",
                GenreId = input.GenreId,
                Name = input.Name ?? "",
                Title = input.Title,
                Price = input.Price ?? 0,
                Quantity = input.Quantity ?? 0,
                PublicationDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                PublisherId = input.PublisherId,
                BookAuthors = authors
            };
            if (!_repository.IsValidEntity(newBook, out List<ValidationResult> errors))
            {
                var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException($"Data is invalid with these errors: {errorMessage}");
            }
            await _repository.AddAsync(newBook);
            var bookItem = await GetByIdAsync(newBook.Id);
            return bookItem;
        }

        public async Task<BookItem?> DeleteBookAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            await _repository.DeleteAsync(book);
            var bookItem = ConvertToBookItem(book);
            return bookItem;
        }

        public async Task<BookItem?> UpdateBookAsync(Book updatedBook)
        {
            if (!_repository.IsValidEntity(updatedBook, out List<ValidationResult> errors))
            {
                var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException("Data is invalid with these errors" + errorMessage);
            }
            var existingBook = await _repository.GetByIdAsync(updatedBook.Id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = updatedBook.Title;
            existingBook.Name = updatedBook.Name;
            existingBook.ShortDescription = updatedBook.ShortDescription;
            existingBook.Price = updatedBook.Price;
            existingBook.Quantity = updatedBook.Quantity;
            existingBook.PublicationDate = updatedBook.PublicationDate;
            existingBook.GenreId = updatedBook.GenreId;
            existingBook.PublisherId = updatedBook.PublisherId;
            existingBook.BookAuthors = updatedBook.BookAuthors;
            await _repository.UpdateAsync(existingBook);
            var updatedBookItem = ConvertToBookItem(existingBook);
            return updatedBookItem;
        }
    }
}
