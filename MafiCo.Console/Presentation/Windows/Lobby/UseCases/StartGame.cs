using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

public class StartGame {
    private readonly GameService _gameService;

    public StartGame(GameService gameService) {
        _gameService = gameService;
    }

    public async Task ExecuteAsync() {
        await _gameService.Start();
    }
}
