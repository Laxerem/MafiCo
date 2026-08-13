using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

public class CreateBot {
    private readonly BotService _service;

    public CreateBot(BotService service) {
        _service = service;
    }

    public async Task ExecuteAsync(string name, Guid llmId) {
        await _service.CreateBot(name, llmId);
    }
}
