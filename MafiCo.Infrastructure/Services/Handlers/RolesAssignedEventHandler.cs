using MafiCo.Domain.Events.Game;
using MafiCo.Infrastructure.Interfaces.Stores;
using MediatR;

namespace MafiCo.Infrastructure.Services.Handlers;

public class RolesAssignedEventHandler : INotificationHandler<RolesAssignedEvent> {
    private readonly IGameStore _gameStore;

    public RolesAssignedEventHandler(IGameStore gameStore) {
        _gameStore = gameStore;
    }

    public Task Handle(RolesAssignedEvent notification, CancellationToken cancellationToken) {
        var game = _gameStore.GetGame() ?? throw new InvalidOperationException("Game not found");
        game.SetRoles(notification);
        return Task.CompletedTask;
    }
}
