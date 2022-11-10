namespace the80by20.Modules.Users.App.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    public DateTime CreatedAt { get; set; }
}