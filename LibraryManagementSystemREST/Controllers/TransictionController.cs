using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransictionController : ControllerBase
    {
        private readonly ITransictionService _transictionService;

        public TransictionController(ITransictionService transictionService)
        {
            _transictionService = transictionService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetTransactions()
        {
            var transactions = _transictionService.GetTransictions();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _transictionService.GetTransictionById(id);
            if (transaction == null)
                return NotFound($"Transaction with id {id} not found.");
            return Ok(transaction);
        }

        [HttpPost("issue")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult IssueBook(IssueTransictionDto dto)
        {
            var result = _transictionService.AddTransiction(dto);
            return Ok(result);
        }

        [HttpPost("return/{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult ReturnBook(int id, [FromBody] ReturnTransictionDto dto)
        {
            var result = _transictionService.UpdateTransiction(id, dto);
            if (result == null)
                return NotFound($"Transaction with id {id} not found.");
            return Ok(result);
        }
    }
}