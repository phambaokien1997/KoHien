using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
	public class AccountController : Controller
	{
        public AccountController()
        {

		}
		public IActionResult Login()
		{
			return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "oidc");
		}

		public IActionResult Logout()
		{
			return SignOut(new AuthenticationProperties { RedirectUri = "/" },
						   CookieAuthenticationDefaults.AuthenticationScheme,
						   "oidc");
		}
	}
}
