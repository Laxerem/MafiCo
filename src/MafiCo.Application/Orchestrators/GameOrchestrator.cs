using MafiCo.Domain.Entities;

namespace MafiCo.Application.Orchestrators;

public class GameOrchestrator {
    private readonly Game _game;
    private readonly GameOutput _output;
    
    public GameOrchestrator(Game game, GameOutput output) {
        _game = game;
        _output = output;
    }

    public async Task SetupGameAsync() {
        
    }
}