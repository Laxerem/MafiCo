using MafiCo.Application.Database;
using MafiCo.Application.Exceptions;
using MafiCo.Domain.Entities;
using MafiCo.Domain.Exceptions;

namespace MafiCo.Application.Orchestrators;

public class GameOrchestrator {
    private readonly Game _game;
    private readonly GameMemory _gameMemory;
    private readonly GameOutput _output;
    
    public GameOrchestrator(Game game, GameMemory gameMemory, GameOutput output) {
        _game = game;
        _gameMemory = gameMemory;
        _output = output;
    }

    public async Task SetupGameAsync() {
        await _gameMemory.InitializeAsync();
        var user = await _gameMemory.GetHumanProfileAsync();
        var bots = await _gameMemory.GetBotProfilesAsync();

        if (user == null) {
            user = new HumanProfile(100, "Player");
        }
    }
}