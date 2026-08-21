using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using LibraryManagmentSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Get all books
        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetBooks()
        {
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        // Get book by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound($"Book with id {id} not found.");
            }
            return Ok(book);
        }

        // Add new book
        [HttpPost]
        public IActionResult AddBook(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                PublishedYear = dto.PublishedYear,
                TotalCopies = dto.TotalCopies,
                AvailableCpoies = dto.AvailableCpoies,
                AuthorId = dto.AuthorId
            };
            var created = _bookService.AddBook(book);
            return Ok(created);
        }

        // Update book
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult UpdateBook(int id, UpdateBookDto dto)
        {
            var result = _bookService.UpdateBook(id, dto.AvailableCpoies);
            if (result == null)
            {
                return NotFound($"Book with id {id} not found.");
            }
            return Ok(result);
        }

        // Delete book
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult DeleteBook(int id)
        {
            var result = _bookService.DeleteBook(id);
            if (result == null)
            {
                return NotFound($"Book with id {id} not found.");
            }
            return Ok(result);
        }
    }
}