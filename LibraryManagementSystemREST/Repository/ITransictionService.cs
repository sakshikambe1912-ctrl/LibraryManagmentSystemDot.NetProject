using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repository
{
    public interface ITransictionService
    {
        List<Transiction> GetTransictions();

        Transiction GetTransictionById(int id);

        Transiction AddTransiction(IssueTransictionDto dto);

        Transiction? UpdateTransiction(int id, ReturnTransictionDto dto);
    }
}