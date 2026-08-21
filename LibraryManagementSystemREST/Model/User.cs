using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(10, ErrorMessage = "10 max letter can be stored in usernmae")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, ErrorMessage = "Maximum 10 characters are allowed")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        [StringLength(10, ErrorMessage = "10 max letter can be stored in role")]
        public string Role { get; set; } = string.Empty;
    }
}
