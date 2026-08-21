using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.DTOs
{
    public class IssueTransictionDto
    {
        [Required(ErrorMessage = "Book Id is mandatory.")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Member Id is mandatory.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Due Date is mandatory.")]
        public DateTime DueDate { get; set; }
    }
}
