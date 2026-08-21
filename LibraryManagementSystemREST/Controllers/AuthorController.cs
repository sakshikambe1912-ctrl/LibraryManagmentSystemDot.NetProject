
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;


namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Get all authors
        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetAuthors()
        {
            var authors = _authorService.GetAuthors();
            return Ok(authors);
        }

        // Get author by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound("Author Not Found.");
            return Ok(author);
        }

        // Add author
        [HttpPost]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult AddAuthor(Author author)
        {
            var result = _authorService.AddAuthor(author);
            return Ok(result);
        }

        // Update author
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            

            var result = _authorService.UpdateAuthor(id,author);
            if (result == null)
                return NotFound("Author Not Found");
            return Ok(result);
        }

        // Delete author
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAuthor(int id)
        {
            var result = _authorService.DeleteAuthor(id);
            if (result == null)
                return NotFound("Author Not Found");
            return Ok(result);
        }
    }
}