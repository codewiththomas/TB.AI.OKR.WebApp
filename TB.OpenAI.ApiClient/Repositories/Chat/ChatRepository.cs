using System.Net.Http.Json;
using System.Text.Json;
using TB.OpenAI.ApiClient.Contracts.Chat;

namespace TB.OpenAI.ApiClient.Repositories.Chat;

public class ChatRepository : BaseRepository, IChatRepository
{
    public async Task<CreateChatCompletionResponse> CreateChatCompletionAsync(CreateChatCompletionRequest createChatCompletionRequest)
    {
        var httpClient = GetAuthenticatedHttpClient();

        var requestUrl = $"{httpClient.BaseAddress}v1/chat/completions";

        var response = await httpClient.PostAsJsonAsync(requestUrl, createChatCompletionRequest);
        var responseContent = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<CreateChatCompletionResponse>(responseContent);

        if (result is null)
        {
            result = new CreateChatCompletionResponse();
            result.DebugInfos.Add($"Result war null. Response content: {responseContent}");
        }

        result.DebugInfos.Add($"Request URL: {requestUrl}");
        result.DebugInfos.Add($"Request content: {JsonSerializer.Serialize(createChatCompletionRequest)}");
        result.DebugInfos.Add($"ApiKey: {OpenAiApiSettings.ApiKey}");
        result.DebugInfos.Add($"Content: {responseContent}");

        return result!;
    }
}
