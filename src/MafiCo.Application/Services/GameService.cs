using MafiCo.Application.Interfaces;
using MafiCo.Domain.Agregate;
using MafiCo.Domain.Entities;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Application.Services;

public class GameService {
    private readonly Game _game;
    private readonly Dictionary<string, IPlayerBrain> _brains;

    public GameService(IEnumerable<(string name, IPlayerBrain brain)> brains) {
        _game = new Game();
        _brains = brains.ToDictionary(x => x.name, p => p.brain);
    }

    public async Task SetupGameAsync() {
        var determineRoleEvents = _game.SetupGame(_brains.Keys.ToList());
        
    }
    
    public async Task StartGameAsync() {
        
    }
}