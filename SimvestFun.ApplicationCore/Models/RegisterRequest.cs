using System.ComponentModel.DataAnnotations;

namespace SimvestFun.ApplicationCore.Models
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}