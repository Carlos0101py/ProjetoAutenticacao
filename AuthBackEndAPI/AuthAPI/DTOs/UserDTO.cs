using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class UserDTO
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        public string RePassword { get; set; }
    }

}