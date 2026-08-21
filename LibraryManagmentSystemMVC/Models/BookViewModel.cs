using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystemMVC.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is mandatory")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Book title must be between 3 to 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Published year is mandatory")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Total copies is mandatory")]
        [Range(1, 50, ErrorMessage = "Total copies must be between 1 and 50")]
        public int TotalCopies { get; set; }

        [Required(ErrorMessage = "Available copies is mandatory")]
        public int AvailableCpoies { get; set; }

        [Required(ErrorMessage = "Author is mandatory")]
        public int AuthorId { get; set; }

        // Populated by the API response (nested Author object) - used for display only.
        public AuthorViewModel? Author { get; set; }
    }
}
