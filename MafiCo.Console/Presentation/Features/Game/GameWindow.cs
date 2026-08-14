using MafiCo.Console.Presentation.Base;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game;

public class GameWindow : Window {
    public override Task Show() {
        AnsiConsole.Console.Write("ИГРА НАЧАЛАСЬ");
        return Task.CompletedTask;
    }
}