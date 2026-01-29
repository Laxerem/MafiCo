using MafiCo.Application.Interfaces;
using MafiCo.Domain.Agregate;
using MafiCo.Domain.Entities;

namespace MafiCo.Application.Services;

public class GameService {
    private readonly Dictionary<string, IPlayerBrain> _brains;

    public GameService(IEnumerable<(string name, IPlayerBrain brain)> brains) {
        _brains = brains.ToDictionary(x => x.name, p => p.brain);
    }
    
    public async Task SetupGameAsync() {
        var game = new Game();
        await game.StartAsync(_brains.Keys.ToList());
    }
}