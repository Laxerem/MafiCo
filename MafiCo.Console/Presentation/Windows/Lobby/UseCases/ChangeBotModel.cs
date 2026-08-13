using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

public class ChangeBotModel {
    private readonly BotService _service;

    public ChangeBotModel(BotService service) {
        _service = service;
    }

    public async Task ExecuteAsync(Guid botId, Guid llmId) {
        await _service.ChangeBotModel(botId, llmId);
    }
}
