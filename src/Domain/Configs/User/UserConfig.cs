using System.ComponentModel.DataAnnotations;

namespace Domain.Configs.User;

public record UserConfig
{
	[Required(ErrorMessage = "UserConfig - Api baseUrl should not be null/empty.")]
	public string? BaseURL { get; set; }
}