using MafiCo.Domain.Events.Profile;
using MediatR;

namespace MafiCo.Infrastructure.Handlers;

public class UsernameChangedEventHandler : INotificationHandler<UsernameChangedEvent> {
    private readonly IMediator _mediator;

    public UsernameChangedEventHandler(IMediator mediator) {
        _mediator = mediator;
    }
    
    public async Task Handle(UsernameChangedEvent notification, CancellationToken cancellationToken) {
        await _mediator.Publish(new UINameChangedEvent(), cancellationToken);
    }
}