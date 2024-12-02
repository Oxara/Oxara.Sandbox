using System.Text.Json.Serialization;

namespace ETag.Delta;

public partial class UserDTO
{
    // Entity Properties
    public Guid UUID { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserFullName { get; set; }
    public string UserEmail { get; set; }
    public DateOnly Birthday { get; set; }
    public int Age { get; set; }

    // Foreign Relation
    public IEnumerable<UserContactDTO> UserContacts { get; set; }
}
