using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystemMVC.Models
{
    public class ReturnTransictionViewModel
    {
        public int Id { get; set; }

        [Range(0, 100000, ErrorMessage = "Fine amount cannot be negative.")]
        public decimal FineAmount { get; set; }
    }
}
