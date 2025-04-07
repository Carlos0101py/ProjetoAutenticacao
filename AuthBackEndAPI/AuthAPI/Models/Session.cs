using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthAPI.Models
{
    public class Session : ModelBase
    {
        [Required]
        public string Token {get; set;}
        [Required]
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]        
        public User User {get; set;}
        

        public Session() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now)
        {
        }
        public Session(Guid id, string token, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Token = token;
        }
    }
}