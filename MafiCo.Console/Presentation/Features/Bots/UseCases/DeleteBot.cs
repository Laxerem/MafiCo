using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Features.Bots.UseCases;

public class DeleteBot {
    private readonly BotService _service;

    public DeleteBot(BotService service) {
        _service = service;
    }

    public async Task ExecuteAsync(Guid botId) {
        await _service.DeleteBot(botId);
    }
}
