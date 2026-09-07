using MafiCo.Application.Game;
using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Notifications;
using MafiCo.Application.Notifications.GameNotifications;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;
using MafiCo.Infrastructure.Controllers;
using MafiCo.Infrastructure.MediatR.System;
using MediatR;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Infrastructure.MediatR.Game;

public class GameOrchestrator : IGameOrchestrator {
    private readonly IMediator  _mediator;
    private readonly IEventConsumer _eventConsumer;
    private readonly GameAggregate _game;
    private readonly Dictionary<Guid, PlayerProcessor> _processors;
    
    public GameOrchestrator(GameAggregate game, IEventConsumer eventConsumer, IMediator mediator) {
        _mediator = mediator;
        _eventConsumer = eventConsumer;
        _game = game;
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
            await pair.Value.SendNotify(new RoleAssignedNotification(_game.CheckRole(pair.Key)));
        }
        
        while (true) {
            await _eventConsumer.SendEvent(new PhaseChangedNotification(_game.Phase));
            await ProcessPhase(_game.Phase);
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }

    private async Task ProcessPhase(GamePhase phase) {
        switch (phase) {
            case GamePhase.Day:
                foreach (var pair in _processors) {
                    var playerId = pair.Key;
                    var processor = pair.Value;
                    await processor.SendNotify(new ControllerChangedNotification(new DefaultController(playerId, _mediator)));
                }
                break;
        }
    }

    public void StopAll() {
        foreach (var keyValuePair in _processors) {
            keyValuePair.Value.Stop();
        }
    }
}