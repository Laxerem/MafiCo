using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Features.Llm.UseCases;

public class DeleteLlmModel {
    private readonly LlmService _service;

    public DeleteLlmModel(LlmService service) {
        _service = service;
    }

    public async Task ExecuteAsync(Guid id) {
        await _service.DeleteAsync(id);
    }
}
