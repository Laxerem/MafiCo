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
        var session = _gameContext.Session!;
        
        foreach (var processor in session.GetProcessors()) {
            var playerRole = session.Game.CheckRole(processor.Id);
            await processor.SendNotify(new RoleAssignedNotification(playerRole));
        }
    }
}