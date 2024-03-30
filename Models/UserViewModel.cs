using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
