using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
		}

		[HttpGet]
		public IActionResult Contact()
		{
			return View();
		}

	}
}
