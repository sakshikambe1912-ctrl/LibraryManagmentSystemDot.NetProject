using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class Author
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="Name is Manadatory.")]
        [StringLength(50,MinimumLength =1,ErrorMessage ="Name must be between 1 to 50 characters.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is Manadatory.")]
        [EmailAddress(ErrorMessage ="Email is invalid.")]
        public string Email {  get; set; }


        [Required(ErrorMessage = "Books is Manadatory.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = " Book Name must be between 1 to 50 characters.")]
        public string Books {  get; set; }
    }
}
