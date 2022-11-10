using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Users.Domain.UserEntity;

[EntityDdd]
public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public FullName FullName { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; private set; }
    public bool IsActive { get; private set; }

    public User(UserId id, Email email, Username username, Password password, FullName fullName, Role role,
        DateTime createdAt, Dictionary<string, IEnumerable<string>> claims, bool isActive)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        FullName = fullName;
        Role = role;
        CreatedAt = createdAt;
        Claims = claims;
        IsActive = isActive;
    }
}