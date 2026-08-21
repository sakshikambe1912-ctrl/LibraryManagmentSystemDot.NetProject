using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.DTOs
{
    public class UpdateMemberDto
    {
        [Required(ErrorMessage = "Phone No is Mandatory.")]
        [Range(1000000000, 9999999999)]
        public long Phoneno { get; set; }
    }
}
