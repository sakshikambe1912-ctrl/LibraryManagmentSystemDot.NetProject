using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is mandatory")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "book title must be between 3 to 150")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Book published year is mandatory")]
        public int PublishedYear { get; set; }


        [Required(ErrorMessage = "Total copies is mandatory")]
        [Range(1, 50)]
        public int TotalCopies { get; set; }


        [Required(ErrorMessage = "Avaialble copies is mandatory")]
        public int AvailableCpoies { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Transiction> Transictions { get; set; } = new List<Transiction>();
    }
}
