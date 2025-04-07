using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthAPI.Models
{
    public class User : ModelBase
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        private string _Password;
        
        [Required]
        [JsonIgnore]
        public string Password
        {
            get => _Password;
            set
            {
                _Password = HashPassword(value);
            }
        }

        public Session Session {get; set;}


        public User() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now)
        {
        }

        public User(Guid id, string userName, string email, string password,
        DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}