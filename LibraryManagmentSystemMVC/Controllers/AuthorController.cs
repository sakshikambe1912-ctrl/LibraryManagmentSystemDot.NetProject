using LibraryManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystemMVC.Controllers
{
    public class AuthorController : BaseApiController
    {
        public AuthorController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync("api/Author");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View(new List<AuthorViewModel>());
            }

            var authors = await response.Content.ReadFromJsonAsync<List<AuthorViewModel>>();

            return View(authors ?? new List<AuthorViewModel>());
        }

        // GET: Author/Create
        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.PostAsJsonAsync("api/Author", author);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Unable to add author. {error}");

            return View(author);
        }

        // GET: Author/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Author/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var author = await response.Content.ReadFromJsonAsync<AuthorViewModel>();
            if (author == null) return NotFound();

            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorViewModel author)
        {
            if (id != author.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(author);
            }

            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.PutAsJsonAsync($"api/Author/{id}", author);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Error {response.StatusCode} - {error}");

            return View(author);
        }

        // GET: Author/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Author/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var author = await response.Content.ReadFromJsonAsync<AuthorViewModel>();
            if (author == null) return NotFound();

            return View(author);
        }

        // POST: Author/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.DeleteAsync($"api/Author/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index");
        }
    }
}
