using System.Text.Json.Serialization;

namespace ETag.Delta;

public class UserContactDTO
{
    // Entity Properties
    public Guid UUID { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }

    // Foreign Relation
    public UserDTO User { get; set; }
    public Guid UserUUID { get; set; }
}
