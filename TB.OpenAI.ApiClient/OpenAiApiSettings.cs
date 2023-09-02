namespace TB.OpenAI.ApiClient;

public class OpenAiApiSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;

    public string? OrganizationId { get; set; }
}
