using System.ComponentModel.DataAnnotations;

namespace BPOSolution.ViewModels
{
    public class BPOClientViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [StringLength(13, MinimumLength = 7)]
        public string ContactNumber { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
