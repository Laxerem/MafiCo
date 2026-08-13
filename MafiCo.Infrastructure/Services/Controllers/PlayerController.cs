using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Infrastructure.Services.Controllers;

public class PlayerController {
    private readonly Guid _playerId;
    private readonly Game _game;
    
    public PlayerController(Guid playerId, Game game) {
        _playerId = playerId;
        _game = game;
    }

    public async Task WaitMove() {
        
    }
}