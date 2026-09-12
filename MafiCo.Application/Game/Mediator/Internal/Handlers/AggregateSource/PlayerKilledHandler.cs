using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate.Events;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers.AggregateSource;

public class PlayerKilledHandler : INotificationHandler<PlayerKilledDomainEvent> {
    private readonly INotifyConsumer _consumer;
    private readonly IProfileRepository _repository;
    
    public PlayerKilledHandler(INotifyConsumer consumer, IProfileRepository repository) {
        _consumer = consumer;
        _repository = repository;
    }
    
    public async Task Handle(PlayerKilledDomainEvent evt, CancellationToken cancellationToken) {
        var playerProfile = await _repository.GetAsync(evt.PlayerId);
        if (playerProfile is null) throw new NullReferenceException($"Player {evt.PlayerId} doesn't exist");
        
        await _consumer.SendNotify(new PlayerKilledNotification(playerProfile.Name));
    }
}