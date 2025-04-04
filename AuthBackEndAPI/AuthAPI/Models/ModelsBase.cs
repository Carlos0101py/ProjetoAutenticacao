using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public abstract class ModelBase
    {
        [Key]
        [Required]
        public Guid Id = Guid.NewGuid();
        [Required]
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        [Required]
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        

        public ModelBase(Guid id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}