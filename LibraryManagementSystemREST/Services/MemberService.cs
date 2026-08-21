using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;

namespace LibraryManagmentSystem.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext context;

        public MemberService(AppDbContext context)
        {
            this.context = context;
        }

        public Member AddMember(CreateMemberDto dto)
        {
            var member = new Member
            {
                Name = dto.Name,
                Email = dto.Email,
                Phoneno = dto.Phoneno,
                IssueDate = dto.IssueDate
            };
            context.Members.Add(member);
            context.SaveChanges();
            return member;
        }

        public Member? DeleteMember(int id)
        {
            var member = context.Members.Find(id);
            if (member == null)
            {
                return null;
            }
            context.Members.Remove(member);
            context.SaveChanges();
            return member;
        }

        public Member GetMemberById(int id)
        {
            return context.Members.Find(id);
        }

        public List<Member> GetMembers()
        {
            return context.Members.ToList();
        }

        public Member UpdateMember(int id, UpdateMemberDto dto)
        {
            var existingMember = context.Members.Find(id);
            if (existingMember == null)
            {
                return null;
            }
            existingMember.Phoneno = dto.Phoneno;
            context.SaveChanges();
            return existingMember;
        }
    }
}