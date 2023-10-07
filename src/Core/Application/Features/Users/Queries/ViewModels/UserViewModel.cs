using Domain.Enums;

namespace Application.Features.Users.Queries.ViewModels;

public record UserViewModel
{
    public Guid Id           { get; set; }
    public string FirstName  { get; set; } = null!;
    public string LastName   { get; set; } = null!;
    public string UserId     { get; set; } = null!;
    public int? Mobile       { get; set; }
    public int? Phone        { get; set; }
    public string? Address   { get; set; }
    public UserStatus Status { get; set; }
    public string Gender     { get; set; } = null!;
}