using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.DTOs
{
    public class CreateMemberDto
    {
        [Required(ErrorMessage = "Name is Mandatory.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 to 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Mandatory.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone No is Mandatory.")]
        [Range(1000000000, 9999999999)]
        public long Phoneno { get; set; }

        [Required(ErrorMessage = "Issue Date is Mandatory.")]
        public DateTime IssueDate { get; set; }
    }
}
