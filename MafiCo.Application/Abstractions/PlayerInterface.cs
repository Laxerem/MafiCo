using MafiCo.Application.Game;
using MafiCo.Application.Interfaces.Controllers;

namespace MafiCo.Application.Abstractions;

public abstract class PlayerInterface {
    protected readonly PlayerContext Context;
    protected readonly IPlayerController? PlayerController;
    
    public PlayerInterface(PlayerContext context, IPlayerController? playerController) {
        Context = context;
        PlayerController = playerController;
    }
}