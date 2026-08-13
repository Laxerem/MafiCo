using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

public class CreateLlm {
    private readonly LlmService _llmService;

    public CreateLlm(LlmService llmService) {
        _llmService = llmService;
    }

    public async Task ExecuteAsync(string modelName, string url, string apiKey) {
        await _llmService.AddAsync(modelName, url, apiKey);
    }
}
