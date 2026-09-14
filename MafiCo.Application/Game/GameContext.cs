using MafiCo.Application.Interfaces.Notifications;
using MediatR;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;
namespace MafiCo.Application.Game;

public class GameContext {
    private bool _isInitialized;
    public GameSession? Session { get; private set; }
    public GameContext() {
        _isInitialized = false;
    }

    internal void SetSession(GameSession session) {
        if (_isInitialized) throw new InvalidOperationException("Game has already been initialized.");
        Session = session;
        _isInitialized = true;
    }

    internal void Reset() {
        Session = null;
        _isInitialized = false;
    }
}