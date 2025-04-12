using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class ChengeAccountDTO
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        public string RePassword { get; set; }
    }

}