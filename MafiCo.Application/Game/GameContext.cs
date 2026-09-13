using System.Collections.ObjectModel;
using MafiCo.Application.Game.Mediator.Internal.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;
namespace MafiCo.Application.Game;

public class GameContext : INotifySource, INotifyConsumer {
    private GameEntity? _game;
    private Dictionary<Guid, PlayerProcessor> _processors;
    private bool _isInitialized;

    public ReadOnlyDictionary<Guid, PlayerProcessor> Processors;
    public event Func<IGameNotification, Task> OnNotification;

    public GameContext() {
        _isInitialized = false;
        _game = null;
        _processors = new Dictionary<Guid, PlayerProcessor>();
    }

    internal async Task InitializeAsync(GameEntity game, ISender mediator) {
        if (_isInitialized) throw new InvalidOperationException("Game has already been initialized.");
        _game = game;
        _isInitialized = true;

        foreach (var playerId in game.GetAllPlayers()) {
            var processor = await mediator.Send(new CreateProcessorCommand(playerId));
            _processors.Add(playerId, processor);
        }

        Processors = _processors.AsReadOnly();
    }

    public PlayerView GetPlayerView(Guid playerId) {
        return _processors[playerId].View;
    }

    internal GameEntity GetGame() {
        if (_game is null) {
            throw new NullReferenceException("Game context is null");
        }
        return _game;
    }

    internal void Reset() {
        _game = null;
        _isInitialized = false;
        _processors.Clear();
    }

    public async Task SendNotify(IGameNotification domainEvent) {
        if (!_isInitialized) throw new NullReferenceException("Game context isn't initialized");
        await OnNotification.Invoke(domainEvent);
    }
}