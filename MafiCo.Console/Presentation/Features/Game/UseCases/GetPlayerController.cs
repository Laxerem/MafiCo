using MafiCo.Infrastructure.Interfaces.Store;
using MafiCo.Infrastructure.Interfaces.Stores;
using MafiCo.Infrastructure.Services.Processors;

namespace MafiCo.Console.Presentation.Features.Game.UseCases;

public class GetPlayerController {
    private readonly IGameStore _gameStore;
    private readonly IUserStore _userStore;

    public GetPlayerController(IGameStore gameStore, IUserStore userStore) {
        _gameStore = gameStore;
        _userStore = userStore;
    }

    public PlayerProcessor Wait() {
        var game = _gameStore.GetGame() ?? throw new Exception("Game not found");
        var userId = _userStore.GetUserId() ?? throw new Exception("User not found");

        return game.GetControlProcessor(userId);
    }
}
