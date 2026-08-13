using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Features.Llm.UseCases;

public class CreateLlm {
    private readonly LlmService _llmService;

    public CreateLlm(LlmService llmService) {
        _llmService = llmService;
    }

    public async Task ExecuteAsync(string modelName, string url, string apiKey) {
        await _llmService.AddAsync(modelName, url, apiKey);
    }
}
