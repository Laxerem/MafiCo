using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using MafiCo.Application.Interfaces;

namespace MafiCo.Infrastracture.Clients;

public class OpenRouterClient : IOpenRouterClient {
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://openrouter.ai/api/v1/chat/completions";

    public OpenRouterClient(HttpClient httpClient, string apiKey) {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/mafico");
    }

    public async Task<string> GetResponseAsync(string request, string modelName) {
        var payload = new ChatCompletionRequest {
            Model = modelName,
            Messages = [
                new ChatMessage { Role = "user", Content = request }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(ApiUrl, payload);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();

        return result?.Choices?.FirstOrDefault()?.Message?.Content
            ?? throw new InvalidOperationException("No response from OpenRouter API");
    }
}

internal class ChatCompletionRequest {
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    [JsonPropertyName("messages")]
    public required List<ChatMessage> Messages { get; set; }
}

internal class ChatMessage {
    [JsonPropertyName("role")]
    public required string Role { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }
}

internal class ChatCompletionResponse {
    [JsonPropertyName("choices")]
    public List<Choice>? Choices { get; set; }
}

internal class Choice {
    [JsonPropertyName("message")]
    public ChatMessage? Message { get; set; }
}
