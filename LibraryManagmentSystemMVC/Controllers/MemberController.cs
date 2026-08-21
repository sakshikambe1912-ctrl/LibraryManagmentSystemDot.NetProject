using LibraryManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystemMVC.Controllers
{
    public class MemberController : BaseApiController
    {
        public MemberController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync("api/Member");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View(new List<MemberViewModel>());
            }

            var members = await response.Content.ReadFromJsonAsync<List<MemberViewModel>>();

            return View(members ?? new List<MemberViewModel>());
        }

        // GET: Member/Create
        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            return View(new MemberViewModel { IssueDate = DateTime.Today });
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var payload = new
            {
                member.Name,
                member.Email,
                member.Phoneno,
                member.IssueDate
            };

            var response = await client.PostAsJsonAsync("api/Member", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Unable to add member. {error}");

            return View(member);
        }

        // GET: Member/Edit/5  (API only allows updating Phoneno)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Member/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var member = await response.Content.ReadFromJsonAsync<MemberViewModel>();
            if (member == null) return NotFound();

            return View(member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MemberViewModel member)
        {
            if (id != member.Id) return BadRequest();

            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            // Only Phoneno is editable - matches UpdateMemberDto on the API.
            var payload = new { member.Phoneno };

            var response = await client.PutAsJsonAsync($"api/Member/{id}", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Error {response.StatusCode} - {error}");

            return View(member);
        }

        // GET: Member/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.GetAsync($"api/Member/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var member = await response.Content.ReadFromJsonAsync<MemberViewModel>();
            if (member == null) return NotFound();

            return View(member);
        }

        // POST: Member/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            if (client == null) return RedirectToAction("Login", "Account");

            var response = await client.DeleteAsync($"api/Member/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index");
        }
    }
}
