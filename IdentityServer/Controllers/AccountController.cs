using BookStore.IdentityServer.Data;
using BookStore.IdentityServer.ViewModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IIdentityServerInteractionService identityServerInteractionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interactionService = identityServerInteractionService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Optionally add claims or roles here
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ViewData["Errors"] = ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage)
                                    .ToList();
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // GET: Logout
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(); // Sign out from cookie authentication

            // If no post-logout redirect URI, return to home
            if (string.IsNullOrEmpty(logoutRequest?.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }

            // Redirect to post-logout URI
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }

}
