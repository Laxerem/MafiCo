using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;

namespace MafiCo.Console.Presentation.Windows.Game;

public class GameWindow : Window {
    private readonly StartGame _startGame;

    public GameWindow(StartGame startGame) {
        _startGame = startGame;
    }

    public async override Task Show() {
        await _startGame.ExecuteAsync();
    }
}
