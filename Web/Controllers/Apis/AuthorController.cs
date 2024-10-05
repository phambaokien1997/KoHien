using BookStore.Core.Models;
using BookStore.Core.Services;
using Microsoft.AspNetCore.Mvc;
using BookStore.Core.Dtos;
using BookStore.Core.Database;
namespace BookStore.Web.Controllers.Apis

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorItem>>> GetAuthors()
        {
            try
            {
                var authors = await _authorService.GetAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorItem>> GetAuthor(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound(new { message = "Author not found." });
            }
            return Ok(author);
        }
        [HttpPost]
        public async Task<ActionResult<AuthorItem>> CreateAuthor([FromBody] AuthorItemDto newAuthorItemDto)
        {
            if(newAuthorItemDto == null|| string.IsNullOrEmpty(newAuthorItemDto.Name))
            {
                return BadRequest(new { message = "Invalid author data." });
            }
            try
            {
                var newAuthor = new AuthorItem
                {
                    Name = newAuthorItemDto.Name,
                    DateOfBirth = newAuthorItemDto.DateOfBirth,
                    Gender = newAuthorItemDto.Gender,
                    Country = newAuthorItemDto.Country,
                    PhoneNumber = newAuthorItemDto.PhoneNumber,
                    BookIds = newAuthorItemDto.BookIds,
                    GenreIds = newAuthorItemDto.GenreId,
                };
                var authorItem = await _authorService.CreateAuthorAsync(newAuthor);
                return CreatedAtAction(nameof(GetAuthor), new { id = authorItem.Id}, authorItem);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.DeleteAuthorAsync(id);
            if (author == null)
            {
                return NotFound(new { message = "Author not found." });
            }
            return Ok(new {message = "Author deleted successfully." });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorItem>> UpdateAuthor(int id, [FromBody] AuthorItemDto authorItemDto)
        {
            if (authorItemDto == null || string.IsNullOrEmpty(authorItemDto.Name))
            {
                return BadRequest(new { message = "Invalid author data." });
            }
            var updateAuthor = new Author
            {
                Id = id,
                Name = authorItemDto.Name,
                DateOfBirth = DateTime.Parse(authorItemDto.DateOfBirth),
                Gender = authorItemDto.Gender,
                Country=authorItemDto.Country,
                PhoneNumber = authorItemDto.PhoneNumber,
                BookAuthors = authorItemDto.BookIds.Select(b=> new BookAuthor
                {
                    BookId = b,
                }).ToList(),
                AuthorGenres = authorItemDto.GenreId.Select(g=> new AuthorGenre
                {
                    GenreId = g,
                }).ToList()
            };
            var updatedAuthorItem = await _authorService.UpdateAuthorAsync(updateAuthor);
            if(updatedAuthorItem == null)
            {
                return NotFound(new {message = "Author not found"});
            }
            return Ok(updatedAuthorItem);


        }
    }
}
