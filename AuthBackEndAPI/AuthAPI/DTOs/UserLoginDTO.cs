using AuthAPI.Models;

public class UserLoginDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public Session Session { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
