namespace TB.OpenAI.ApiClient.Contracts.Chat;

public interface IChatRepository
{
    Task<CreateChatCompletionResponse> CreateChatCompletionAsync(CreateChatCompletionRequest createChatCompletionRequest);
}
