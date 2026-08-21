namespace LibraryManagmentSystemMVC.Models
{
    // Used only to display data returned by GET api/Transiction
    public class IssueTransictionViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal FineAmount { get; set; }

        // Populated by the API response (nested Book/Member objects) - display only.
        public BookViewModel? Book { get; set; }

        public MemberViewModel? Member { get; set; }
    }
}
