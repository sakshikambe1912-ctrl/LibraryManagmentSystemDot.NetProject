using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.DTOs
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Available Copies is mandatory.")]
        public int AvailableCpoies { get; set; }
    }
}
