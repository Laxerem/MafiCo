using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Domain.Entities.Players;
using MafiCo.Domain.Events.Game;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Services.Processors;

namespace MafiCo.Infrastructure.Services;

public class GameOrchestrator {
    private readonly IGameController _game;
    private readonly HashSet<Guid> _playersIds;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Dictionary<Guid, PlayerProcessor> _processors;
    
    public GameOrchestrator(IGameController gameController, IUnitOfWork unitOfWork) {
        _game = gameController;
        _playersIds = gameController.GetAllPlayers();
        _processors = new Dictionary<Guid, PlayerProcessor>();
        _unitOfWork = unitOfWork;
    }

    public SettingProcessor GetSettingProcessor() {
        return new SettingProcessor(_game, _unitOfWork);
    }

    public PlayerProcessor GetControlProcessor(Guid playerId) => _processors[playerId];

    public void SetRoles(RolesAssignedEvent notification) {
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
    }
}
