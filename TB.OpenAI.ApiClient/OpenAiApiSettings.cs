namespace TB.OpenAI.ApiClient;

internal static class OpenAiApiSettings
{
    public static string ApiKey { get; set; } = string.Empty;

    public static string BaseUrl { get; set; } = string.Empty;

    public static string? OrganizationId { get; set; }
}
