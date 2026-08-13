using MafiCo.Infrastructure.DTOs;
using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

public class GetLlmModels {
    private readonly LlmService _service;

    public GetLlmModels(LlmService service) {
        _service = service;
    }

    public async Task<List<LlmDto>> ExecuteAsync() {
        return await _service.GetAllAsync();
    }
}
