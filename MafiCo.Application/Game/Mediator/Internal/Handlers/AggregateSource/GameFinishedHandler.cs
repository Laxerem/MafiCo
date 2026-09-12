using MafiCo.Application.Game.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate.Events;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers.AggregateSource;

public class GameFinishedHandler : INotificationHandler<GameFinishedEvent> {
    private readonly GameContext _context;
    
    public GameFinishedHandler(GameContext context) {
        _context = context;
    }
    
    public async Task Handle(GameFinishedEvent notification, CancellationToken cancellationToken) {
        await _context.SendNotify(new GameFinishedNotification(notification.Winners, notification.Losers));
        _context.Reset();
    }
}