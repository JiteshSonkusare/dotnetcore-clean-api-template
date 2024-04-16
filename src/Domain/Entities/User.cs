using Domain.Enums;
using Domain.Contracts;

namespace Domain.Entities;

public class User : AuditableEntity<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int? Mobile { get; set; }
    public int? Phone { get; set; }
    public string? Address { get; set; }
    public UserStatus Status { get; set; }
    public string? Gender { get; set; }
}