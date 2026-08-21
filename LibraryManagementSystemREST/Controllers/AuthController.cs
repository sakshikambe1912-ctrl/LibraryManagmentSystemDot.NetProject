using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = service.Login(request.UserName, request.Password);

            if (token == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new { token = token });
        }
    }
}
