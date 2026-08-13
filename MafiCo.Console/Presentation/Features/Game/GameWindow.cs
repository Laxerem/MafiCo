using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Features.Game.UseCases;

namespace MafiCo.Console.Presentation.Features.Game;

public class GameWindow : Window {
    private readonly StartGame _startGame;

    public GameWindow(StartGame startGame) {
        _startGame = startGame;
    }

    public async override Task Show() {
        await _startGame.ExecuteAsync();
    }
}
