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
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult GetMembers()
        {
            var members = _memberService.GetMembers();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var member = _memberService.GetMemberById(id);
            if (member == null)
                return NotFound($"Member with id {id} not found.");
            return Ok(member);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public IActionResult AddMember(CreateMemberDto dto)
        {
            var result = _memberService.AddMember(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public IActionResult UpdateMember(int id, UpdateMemberDto dto)
        {
            var result = _memberService.UpdateMember(id, dto);
            if (result == null)
                return NotFound($"Member with id {id} not found.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMember(int id)
        {
            var result = _memberService.DeleteMember(id);
            if (result == null)
                return NotFound($"Member with id {id} not found.");
            return Ok(result);
        }
    }
}