using BookStore.Core.Database;
using BookStore.Core.Dtos;
using BookStore.Core.Models;
using BookStore.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksApiController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookItem>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetBooksAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookItem>> GetBook(int id)
        {
            var bookItem = await _bookService.GetByIdAsync(id);
            if (bookItem == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(bookItem);
        }

        [HttpPost]
        public async Task<ActionResult<BookItem>> CreateBook([FromBody] BookItemDto newBookDto)// use dto
        {
            if (newBookDto == null || string.IsNullOrEmpty(newBookDto.Title))
            {
                return BadRequest(new { message = "Invalid book data." });
            }
            try
            {
                var newBook = new BookItem
                {
                    Name = newBookDto.Name,
                    Title = newBookDto.Title,
                    Price = newBookDto.Price,
                    GenreId = newBookDto.GenreId,
                    PublisherId = newBookDto.PublisherId,
                    AuthorIds = newBookDto.AuthorIds
                };
                var bookItem = await _bookService.CreateBookAsync(newBook);

                return CreatedAtAction(nameof(GetBook), new { id = bookItem.Id }, bookItem);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookItem>> DeleteBook(int id)
        {
            var bookItem = await _bookService.DeleteBookAsync(id);
            if (bookItem == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(new { message = "Book deleted successfully."/*, book = bookItem */});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookItem>> UpdateBook(int id, [FromBody] BookItemDto updatedBookDto)// dto
        {
            if (updatedBookDto == null || string.IsNullOrEmpty(updatedBookDto.Title) /*|| updatedBookDto.Id != id*/)
            {
                return BadRequest(new { message = "Invalid book data." });
            }
            var updatedBook = new Book
            {
                Id = id, // Sử dụng ID từ route
                Name = updatedBookDto.Name,
                Title = updatedBookDto.Title,
                Price = updatedBookDto.Price,
                GenreId = updatedBookDto.GenreId,
                PublisherId = updatedBookDto.PublisherId,
                // Chuyển đổi AuthorIds sang danh sách BookAuthor
                Authors = updatedBookDto.AuthorIds.Select(authorId => new BookAuthor
                {
                    AuthorId = authorId
                }).ToList()
            };
            var bookItem = await _bookService.UpdateBookAsync(updatedBook);
            if (bookItem == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(bookItem);
        }
    }
}
