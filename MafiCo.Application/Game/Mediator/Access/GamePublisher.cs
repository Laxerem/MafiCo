using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Application.Interfaces.Mediator.Access;
using MediatR;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MafiCo.Application.Game.Mediator.Access;

public class GamePublisher : IGamePublisher {
    private readonly IMediator _mediator;
    private readonly ILogger _logger = Log.ForContext<GamePublisher>();

    public GamePublisher(IMediator mediator) {
        _mediator = mediator;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : IGameEvent {
        _logger.Information("Dispatching game event {EventType}", notification.GetType().Name);
        return _mediator.Publish(notification, cancellationToken);
    }
}
