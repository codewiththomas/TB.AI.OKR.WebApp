using TB.AI.OKR.WebApp.Core.Domain.Abstractions;

namespace TB.AI.OKR.Core.Domain;

public class Review : BaseEntity
{
    public DateTime Created { get; set; }

    public TimeSpan ReviewTime { get; set; }

    public string Provider { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string? Result { get; set; }
}
