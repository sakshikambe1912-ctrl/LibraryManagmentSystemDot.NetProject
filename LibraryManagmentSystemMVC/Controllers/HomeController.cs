using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystemMVC.Controllers
{
    public class HomeController : BaseApiController
    {
        public HomeController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
