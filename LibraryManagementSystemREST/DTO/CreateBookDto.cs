using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.DTOs
{
    public class CreateBookDto
    {
        
        
            [Required, StringLength(150, MinimumLength = 3)]
            public string Title { get; set; }

            [Required(ErrorMessage ="Published Year is mandatory.")]
            public int PublishedYear { get; set; }

            [Required(ErrorMessage = "Total Copies is mandatory.")]
            [ Range(1, 50)]
            public int TotalCopies { get; set; }

            [Required(ErrorMessage = "Available Copies is mandatory.")]
            public int AvailableCpoies { get; set; }

            [Required(ErrorMessage = "Author Id is mandatory.")]
            public int AuthorId { get; set; }
        }
    }

