using BookStore.Core.Models;
using BookStore.Core.Services;
using BookStore.Web.Enum;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService  _genreService;
        private readonly IPublisherService _publisherService;
        public MetadataController(IPublisherService publisherService, IGenreService genreService,
                                   IBookService bookService, IAuthorService authorService)
        {
            _publisherService = publisherService;
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DropdownResult<int>>>> GetDropdownValue(DropdownType type)
        {
            try
            {
                var data = new Dictionary<int,string>();
                switch (type)
                {
                    case DropdownType.Authors:
                        data = await _authorService.GetNamesAsync();
                        break;
                    case DropdownType.Genre:
                        data = await _genreService.GetNamesAsync();
                        break;
                    case DropdownType.Publisher:
                        data = await _publisherService.GetNamesAsync();
                        break;
                    default:
                        break;
                }
                var result = data.Select(x => new DropdownResult<int>
                {
                    Id = x.Key,
                    Value = x.Value,
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
