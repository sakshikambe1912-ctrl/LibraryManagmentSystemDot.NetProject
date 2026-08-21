using LibraryManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagmentSystemMVC.Controllers
{
    public class BookController : BaseApiController
    {
        public BookController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync("api/Book");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View(new List<BookViewModel>());
            }

            var books = await response.Content.ReadFromJsonAsync<List<BookViewModel>>();

            return View(books ?? new List<BookViewModel>());
        }

        // GET: Book/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            await PopulateAuthorsDropDown(client);

            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                await PopulateAuthorsDropDown(client, book.AuthorId);
                return View(book);
            }

            // The API's CreateBookDto only needs these fields.
            var payload = new
            {
                book.Title,
                book.PublishedYear,
                book.TotalCopies,
                book.AvailableCpoies,
                book.AuthorId
            };

            var response = await client.PostAsJsonAsync("api/Book", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Unable to add book. {error}");

            await PopulateAuthorsDropDown(client, book.AuthorId);
            return View(book);
        }

        // GET: Book/Edit/5  (API only allows updating AvailableCpoies)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Book/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel book)
        {
            if (id != book.Id) return BadRequest();

            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            // Only AvailableCpoies is editable - matches UpdateBookDto on the API.
            var payload = new { book.AvailableCpoies };

            var response = await client.PutAsJsonAsync($"api/Book/{id}", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Error {response.StatusCode} - {error}");

            return View(book);
        }

        // GET: Book/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Book/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Book/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.DeleteAsync($"api/Book/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index");
        }

        private async Task PopulateAuthorsDropDown(HttpClient client, int? selectedId = null)
        {
            var authors = await client.GetFromJsonAsync<List<AuthorViewModel>>("api/Author")
                          ?? new List<AuthorViewModel>();

            ViewBag.Authors = new SelectList(authors, "Id", "Name", selectedId);
        }
    }
}
