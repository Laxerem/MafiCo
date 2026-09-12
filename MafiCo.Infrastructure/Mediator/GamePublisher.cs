using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Application.Interfaces.Mediator.Access;
using MediatR;

namespace MafiCo.Infrastructure.Mediator;

public class GamePublisher : IGamePublisher {
    private readonly IMediator _mediator;
    
    public GamePublisher(IMediator mediator) {
        _mediator = mediator;
    }
    
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : IGameEvent {
        return _mediator.Publish(notification, cancellationToken);
    }
}