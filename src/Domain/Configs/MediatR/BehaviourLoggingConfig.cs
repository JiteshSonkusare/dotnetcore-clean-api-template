namespace Domain.Configs.MediatR;

public record BehaviourLoggingConfig
{
    public const string SectionName = "BehaviourLogging";
    public bool Enable { get; set; } = false;
}
