using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Domain.Entities.Players;
using MafiCo.Domain.Events.Game;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Services.Processors;
using MediatR;

namespace MafiCo.Infrastructure.Services;

public class GameOrchestrator : INotificationHandler<RolesAssignedEvent> {
    private readonly IGameController _game;
    private readonly List<Guid> _playersIds;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Dictionary<Guid, PlayerProcessor> _processors;
    
    public GameOrchestrator(IGameController gameController, List<Guid> playersIds, IUnitOfWork unitOfWork) {
        _playersIds = playersIds;
        _processors = new Dictionary<Guid, PlayerProcessor>();
        _game = gameController;
        _unitOfWork = unitOfWork;
    }

    public SettingProcessor GetSettingProcessor() {
        return new SettingProcessor(_game, _playersIds, _unitOfWork);
    }

    public async Task<PlayerProcessor> GetControlProcessor(Guid playerId) {
        return _processors[playerId];
    }

    public Task Handle(RolesAssignedEvent notification, CancellationToken cancellationToken) {
        foreach (var evt in notification.Events) {
            switch (evt.Role) {
                case Role.Citizen:
                    _processors.Add(evt.Id, new CitizenProcessor(evt.Id, evt.Role, _game));
                    break;
                case Role.Mafia:
                    _processors.Add(evt.Id, new MafiaProcessor(evt.Id, evt.Role, _game));
                    break;
            }
        }
        return Task.CompletedTask;
    }
}