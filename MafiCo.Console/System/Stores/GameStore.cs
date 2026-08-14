using MafiCo.Infrastructure.Interfaces.Stores;
using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.System.Stores;

public class GameStore : IGameStore {
    private GameOrchestrator? _game;

    public void SetGame(GameOrchestrator game) {
        _game = game;
    }

    public GameOrchestrator? GetGame() {
        return _game;
    }

    public void Clear() {
        _game = null;
    }
}
