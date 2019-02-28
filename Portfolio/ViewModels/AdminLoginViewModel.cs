using System.ComponentModel.DataAnnotations;

namespace BPOSolution.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
