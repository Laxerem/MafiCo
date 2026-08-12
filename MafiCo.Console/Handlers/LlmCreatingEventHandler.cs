using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Infrastructure.Services;
using MediatR;

namespace MafiCo.Console.Handlers;

public class LlmCreatingEventHandler : INotificationHandler<CreateLlm> {
    private readonly LlmService _llmService;

    public LlmCreatingEventHandler(LlmService llmService) {
        _llmService = llmService;
    }

    public async Task Handle(CreateLlm evt, CancellationToken cancellationToken) {
        await _llmService.AddAsync(evt.ModelName, evt.Url, evt.ApiKey);
    }
}
