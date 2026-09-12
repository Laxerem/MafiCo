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
    private readonly IServiceScopeFactory _scopeFactory;
    private Guid? _gameId;
    private Dictionary<Guid, PlayerProcessor> _processors;
    private bool _isInitialized;

    public ReadOnlyDictionary<Guid, PlayerProcessor> Processors;
    public event Func<IGameNotification, Task> OnNotification;

    public GameContext(IServiceScopeFactory scopeFactory) {
        _scopeFactory = scopeFactory;
        _isInitialized = false;
        _gameId = null;
        _processors = new Dictionary<Guid, PlayerProcessor>();
    }

    public async Task InitializeAsync(GameEntity game, ISender mediator) {
        if (_isInitialized) throw new InvalidOperationException("Game has already been initialized.");
        _gameId = game.Id;
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

    public async Task<GameEntity> GetGameAsync() {
        if (_gameId is null) {
            throw new NullReferenceException("Game context is null");
        }

        using var scope = _scopeFactory.CreateScope();
        var gameRepository = scope.ServiceProvider.GetRequiredService<IGameRepository>();
        return await gameRepository.GetAsync(_gameId.Value) ?? throw new NullReferenceException("Game not found");
    }

    public void Reset() {
        _gameId = null;
        _isInitialized = false;
        _processors.Clear();
    }

    public async Task SendNotify(IGameNotification domainEvent) {
        if (!_isInitialized) throw new NullReferenceException("Game context isn't initialized");
        var game = await GetGameAsync();
        if (game.FinishedAt is not null) throw new ApplicationException("You cannot send the notify when game finished");
        await OnNotification.Invoke(domainEvent);
    }
}