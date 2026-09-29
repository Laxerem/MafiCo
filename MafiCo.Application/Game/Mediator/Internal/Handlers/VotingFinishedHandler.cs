using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Game.Notifications.Players;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers;

public class VotingFinishedHandler : INotificationHandler<VotingFinishedEvent> {
    private readonly GameContext _context;
    private readonly IProfileRepository _profileRepository;
    
    public VotingFinishedHandler(GameContext gameContext, IProfileRepository profileRepository) {
        _context = gameContext;
        _profileRepository = profileRepository;
    }
    
    public async Task Handle(VotingFinishedEvent notification, CancellationToken cancellationToken) {
        var session = _context.Session!;
        
        if (notification.Victim.HasValue is false) {
            await session.HandleAsync(new PlayerExcludedNotification(null, null));
            return;
        }
        
        var playerId = notification.Victim!.Value!;
        var playerProfile = await _profileRepository.GetAsync(new ProfileId(playerId.Id));
        if (playerProfile is null) {
            throw new NullReferenceException($"Player {playerId} does not exist");
        }
        await session.HandleAsync(new PlayerExcludedNotification(playerId, playerProfile.Name));
    }
}