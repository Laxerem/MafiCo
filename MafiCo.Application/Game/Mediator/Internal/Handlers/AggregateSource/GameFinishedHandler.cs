using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate.Events;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers.AggregateSource;

public class GameFinishedHandler : INotificationHandler<GameFinishedEvent> {
    private readonly GameContext _context;
    private readonly IProfileRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GameFinishedHandler(GameContext context, IProfileRepository repository, IUnitOfWork unitOfWork) {
        _context = context;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(GameFinishedEvent notification, CancellationToken cancellationToken) {
        var gameSession = _context.Session!;
        
        var playerIds = notification.Losers
            .Concat(notification.Winners)
            .Select(x => x.Id)
            .ToList();
        
        var profiles = await _repository.GetRangeAsync(playerIds);
        var dictionary = profiles.ToDictionary(x => x.Id, x => x);
        foreach (var winner in notification.Winners) {
            dictionary[winner.Id].RegisterWin();
        }
        
        await gameSession.HandleAsync(new GameFinishedNotification(notification.Winners, notification.Losers));
        
        gameSession.Finish();
        _context.Reset();
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}