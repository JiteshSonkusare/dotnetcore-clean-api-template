using System.ComponentModel.DataAnnotations;

namespace Domain.Configs;

public class OAuthConfig
{
	public const string SectionName = "OAuth";

	[Required(ErrorMessage = "Oauth Authority URL should not be null/empty.")]
	public string? Authority { get; set; }

	[Required(ErrorMessage = "Oauth Scope should not be null/empty.")]
	public string? Scope { get; set; }

	[Required(ErrorMessage = "Oauth Audience should not be null/empty.")]
	public string? Audience { get; set; }
}