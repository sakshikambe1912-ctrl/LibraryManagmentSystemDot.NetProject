using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystemMVC.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 to 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Books is mandatory.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Book name must be between 1 to 50 characters.")]
        public string Books { get; set; } = string.Empty;
    }
}
