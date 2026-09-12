using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers;

internal class PhaseChangedHandler : INotificationHandler<PhaseChangedEvent> {
    private readonly INotifyConsumer _consumer;
    private readonly IUnitOfWork _unitOfWork;
    
    public PhaseChangedHandler(INotifyConsumer consumer, IUnitOfWork unitOfWork) {
        _consumer = consumer;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(PhaseChangedEvent evt, CancellationToken cancellationToken) {
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        await _consumer.SendNotify(new PhaseChangedNotification(evt.Phase));
    }
}