using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate.Events;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers.AggregateSource;

public class PlayerKilledHandler : INotificationHandler<PlayerKilledDomainEvent> {
    private readonly GameContext _context;
    private readonly IProfileRepository _repository;
    
    public PlayerKilledHandler(GameContext context, IProfileRepository repository) {
        _context = context;
        _repository = repository;
    }
    
    public async Task Handle(PlayerKilledDomainEvent evt, CancellationToken cancellationToken) {
        var gameSession = _context.Session!;
        var playerProfile = await _repository.GetAsync(evt.PlayerId);
        if (playerProfile is null) throw new NullReferenceException($"Player {evt.PlayerId} doesn't exist");
        
        await gameSession.HandleAsync(new PlayerKilledNotification(playerProfile.Name));
    }
}