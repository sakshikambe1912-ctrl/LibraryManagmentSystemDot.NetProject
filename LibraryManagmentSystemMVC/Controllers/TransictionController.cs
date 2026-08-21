using LibraryManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagmentSystemMVC.Controllers
{
    public class TransictionController : BaseApiController
    {
        public TransictionController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        // GET: Transiction
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync("api/Transiction");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View(new List<TransictionViewModel>());
            }

            var transictions = await response.Content.ReadFromJsonAsync<List<TransictionViewModel>>();

            return View(transictions ?? new List<TransictionViewModel>());
        }

        // GET: Transiction/Issue
        [HttpGet]
        public async Task<IActionResult> Issue()
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            await PopulateDropDowns(client);

            return View(new IssueTransictionViewModel());
        }

        // POST: Transiction/Issue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Issue(IssueTransictionViewModel model)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                await PopulateDropDowns(client, model.BookId, model.MemberId);
                return View(model);
            }

            var response = await client.PostAsJsonAsync("api/Transiction/issue", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Unable to issue book. {error}");

            await PopulateDropDowns(client, model.BookId, model.MemberId);
            return View(model);
        }

        // GET: Transiction/Return/5
        [HttpGet]
        public async Task<IActionResult> Return(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Transiction/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var transiction = await response.Content.ReadFromJsonAsync<TransictionViewModel>();
            if (transiction == null) return NotFound();

            ViewBag.Transiction = transiction;

            return View(new ReturnTransictionViewModel { Id = transiction.Id });
        }

        // POST: Transiction/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id, ReturnTransictionViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.PostAsJsonAsync($"api/Transiction/return/{id}", new { model.FineAmount });

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Error {response.StatusCode} - {error}");

            return View(model);
        }

        private async Task PopulateDropDowns(HttpClient client, int? selectedBookId = null, int? selectedMemberId = null)
        {
            var books = await client.GetFromJsonAsync<List<BookViewModel>>("api/Book") ?? new List<BookViewModel>();
            var members = await client.GetFromJsonAsync<List<MemberViewModel>>("api/Member") ?? new List<MemberViewModel>();

            ViewBag.Books = new SelectList(books, "Id", "Title", selectedBookId);
            ViewBag.Members = new SelectList(members, "Id", "Name", selectedMemberId);
        }
    }
}
