using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystemMVC.Models
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 to 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is mandatory.")]
        [Range(1000000000, 9999999999, ErrorMessage = "Enter a valid 10 digit phone number.")]
        public long Phoneno { get; set; }

        [Required(ErrorMessage = "Issue date is mandatory.")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; } = DateTime.Today;
    }
}
