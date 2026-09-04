using MafiCo.Application.Abstractions;
using MafiCo.Application.Game.Contexts;
using MediatR;

namespace MafiCo.Application.Game;

public class PlayerProcessor {
    private readonly GameContext _context;
    private readonly PlayerInterface _interface;
    
    public PlayerProcessor(GameContext context, PlayerInterface playerInterface) {
        _context = context;
        _interface = playerInterface;
    }

    public async Task RunAsync() {
        
    }

    public void Stop() {
        
    }

    private void HandleEvent(INotification evt) {
        
    }
}