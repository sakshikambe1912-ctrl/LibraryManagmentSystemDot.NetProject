using LibraryManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystemMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient("LibraryManagmentSystemAPI");

            var response = await client.PostAsJsonAsync("api/Auth/login", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError("", $"Login failed: {error}");

                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                ModelState.AddModelError("", "API did not return a JWT token.");

                return View(model);
            }

            // Store JWT + username in MVC Session
            HttpContext.Session.SetString("JwtToken", result.Token);
            HttpContext.Session.SetString("UserName", model.UserName);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Login));
        }
    }
}
