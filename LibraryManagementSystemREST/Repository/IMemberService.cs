using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repository
{
    public interface IMemberService
    {
        List<Member> GetMembers();

        Member GetMemberById(int id);

        Member AddMember(CreateMemberDto dto);

        Member UpdateMember(int id, UpdateMemberDto dto);

        Member? DeleteMember(int id);
    }
}
