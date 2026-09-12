using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Game.Notifications;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers;

public class RoleAssignedHandler : INotificationHandler<RoleAssignedEvent> {
    private readonly GameContext _gameContext;
    
    public RoleAssignedHandler(GameContext gameContext) {
        _gameContext = gameContext;
    }
    
    public async Task Handle(RoleAssignedEvent notification, CancellationToken cancellationToken) {
        var game = await _gameContext.GetGameAsync();
        
        foreach (var pair in _gameContext.Processors) {
            var processorId = pair.Key;
            var processor = pair.Value;
            
            var playerRole = game.CheckRole(processorId);
            
            await processor.SendNotify(new RoleAssignedNotification(playerRole));
        }
    }
}