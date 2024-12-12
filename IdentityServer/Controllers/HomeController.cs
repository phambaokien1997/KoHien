using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly IIdentityServerInteractionService _interactionService;

    public HomeController(IIdentityServerInteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    [Route("home/error")]
    public async Task<IActionResult> Error(string errorId)
    {
        var errorContext = await _interactionService.GetErrorContextAsync(errorId);

        var viewModel = new ErrorDetailViewModel
        {
            Error = errorContext?.Error ?? "Unknown error",
            Description = errorContext?.ErrorDescription ?? "No description available",
            RedirectUrl = errorContext?.RedirectUri ?? string.Empty,
            RequestId = HttpContext.TraceIdentifier
        };

        return View(viewModel);
    }
}

public class ErrorDetailViewModel
{
    public string Error { get; set; }
    public string Description { get; set; }
    public string RedirectUrl { get; set; }
    public string RequestId { get; set; }
}
