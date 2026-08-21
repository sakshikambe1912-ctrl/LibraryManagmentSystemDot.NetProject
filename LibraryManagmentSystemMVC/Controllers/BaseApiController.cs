using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LibraryManagmentSystemMVC.Controllers
{
    // Shared helper used by every MVC controller that needs to call the
    // LibraryManagmentSystem REST API. Keeps the "read JWT from session and
    // attach it as a Bearer token" logic in one place.
    public abstract class BaseApiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        protected BaseApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Returns null (and the caller should redirect to Login) if there is no JWT in session.
        protected HttpClient? GetApiClient()
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var client = _httpClientFactory.CreateClient("LibraryManagmentSystemAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        protected bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken"));
        }
    }
}
