using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Services
{
    public class TransictionService : ITransictionService
    {
        private readonly AppDbContext context;

        public TransictionService(AppDbContext context)
        {
            this.context = context;
        }

        public Transiction AddTransiction(IssueTransictionDto dto)
        {
            var transiction = new Transiction
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                IssueDate = DateTime.UtcNow,
                DueDate = dto.DueDate,
                Status = "Issued",
                FineAmount = 0
            };
            context.Transictions.Add(transiction);
            context.SaveChanges();
            return transiction;
        }

        public Transiction GetTransictionById(int id)
        {
            return context.Transictions
                .Include(t => t.Book)
                .Include(t => t.Member)
                .FirstOrDefault(t => t.Id == id);
        }

        public List<Transiction> GetTransictions()
        {
            return context.Transictions
                .Include(t => t.Book)
                .Include(t => t.Member)
                .ToList();
        }

        public Transiction? UpdateTransiction(int id, ReturnTransictionDto dto)
        {
            var existingTransiction = context.Transictions.FirstOrDefault(t => t.Id == id);
            if (existingTransiction == null)
            {
                return null;
            }
            existingTransiction.Status = "Returned";
            existingTransiction.ReturnDate = DateTime.UtcNow;
            existingTransiction.FineAmount = dto.FineAmount;
            context.SaveChanges();
            return existingTransiction;
        }
    }
}