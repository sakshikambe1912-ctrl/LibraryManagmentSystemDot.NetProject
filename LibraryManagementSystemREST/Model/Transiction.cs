using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class Transiction
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; }

        public decimal FineAmount { get; set; }

        public Book Book { get; set; }

        public Member Member { get; set; }
    }
}
