using MafiCo.Console.Presentation.Windows.Lobby.Events;
using MafiCo.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Console.Handlers;

public class GameStartingEventHandler : INotificationHandler<StartGame> {
    private readonly IServiceProvider _services;
    
    public GameStartingEventHandler(IServiceProvider serviceProvider) {
        _services = serviceProvider;
    }
    
    public async Task Handle(StartGame notification, CancellationToken cancellationToken) {
        using var scope = _services.CreateScope();
        var gameService = scope.ServiceProvider.GetRequiredService<GameService>();
        await gameService.Start();
    }
}