using MediatR;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;
namespace MafiCo.Application.Game.Contexts;

public class GameContext {
    private bool _isInitialized;
    private GameEntity? _game;

    public GameContext() {
        _isInitialized = false;
    }

    public void SetGame(GameEntity game) {
        if (_isInitialized) throw new InvalidOperationException("Game has already been initialized.");
        _game = game;
        _isInitialized = true;
    }

    public GameEntity GetGame() {
        if (_game is null) {
            throw new NullReferenceException("Game context is null");
        }
        return _game;
    }
}