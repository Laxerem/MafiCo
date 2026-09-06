using MafiCo.Application.Game;
using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Notifications;
using MafiCo.Infrastructure.MediatR.System;
using MediatR;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Infrastructure.MediatR.Game;

public class GameOrchestrator : IGameOrchestrator {
    private readonly IMediator  _mediator;
    private readonly IEventConsumer _eventConsumer;
    private readonly GameAggregate _game;
    private readonly Dictionary<Guid, PlayerProcessor> _processors;
    
    public GameOrchestrator(IEventConsumer eventConsumer, IMediator mediator) {
        _mediator = mediator;
        _eventConsumer = eventConsumer;
        _processors = new Dictionary<Guid, PlayerProcessor>();
    }

    public async Task Initialize(HashSet<Guid> playerIds) {
        foreach (var playerId in playerIds) {
            var processor = await _mediator.Send(new CreateProcessorCommand(playerId));
            _processors.Add(playerId, processor);
        }
    }

    public PlayerContext GetPlayerContext(Guid playerId) {
        return _processors[playerId].Context;
    }

    public async Task StartAsync(int mafiaCount) {
        foreach (var processor in _processors.Values) {
            await processor.RunAsync();
        }
        _game.AssignRoles(mafiaCount);
        foreach (var pair in _processors) {
            pair.Value.SendNotify(new RoleAssignedNotification(_game.CheckRole(pair.Key)));
        }
    }

    public void StopAll() {
        foreach (var keyValuePair in _processors) {
            keyValuePair.Value.Stop();
        }
    }
}