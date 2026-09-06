using MafiCo.Application.Interfaces;
using MafiCo.Domain.Interfaces;
using MediatR;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;
namespace MafiCo.Application.Game.Contexts;

public class GameContext : INotificationHandler<IDomainEvent>, IEventSource, IEventConsumer {
    private bool _isInitialized;
    private GameEntity? _game;
    private IGameOrchestrator? _gameOrchestrator;
    public event Action<INotification> OnNotification;

    public GameContext() {
        _isInitialized = false;
    }

    public void Initialize(GameEntity game, IGameOrchestrator gameOrchestrator) {
        if (_isInitialized) throw new InvalidOperationException("Game has already been initialized.");
        _game = game;
        _gameOrchestrator = gameOrchestrator;
        _isInitialized = true;
    }

    public GameEntity GetGame() {
        if (_game is null) {
            throw new NullReferenceException("Game context is null");
        }
        return _game;
    }

    public void Reset() {
        _game = null;
        _gameOrchestrator?.StopAll();
        _gameOrchestrator = null;
        _isInitialized = false;
    }

    public Task Handle(IDomainEvent notification, CancellationToken cancellationToken) {
        OnNotification?.Invoke(notification);
        return Task.CompletedTask;
    }

    public Task SendEvent(INotification notification) {
        OnNotification.Invoke(notification);
        return Task.CompletedTask;
    }
}