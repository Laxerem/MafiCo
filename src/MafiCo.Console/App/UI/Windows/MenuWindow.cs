using MafiCo.Console.App.Exceptions;
using MafiCo.Console.App.UI.Common;
using MafiCo.Console.App.UI.Events;
using MafiCo.Console.App.UI.Windows.Game;
using MafiCo.Domain.Exceptions;
using Spectre.Console;

namespace MafiCo.Console.App.UI.Windows;

public class MenuWindow : UiWindow {
    public override async Task Show() {
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        var choice = await WaitChoice("", ["Играть", "Выйти"]);
        switch (choice) {
            case "Играть":
                SwitchTo<StartGameWindow>();
                break;
            case "Выйти":
                throw new GameClosedException("Мафия не ждёт...");
        }
    }
}