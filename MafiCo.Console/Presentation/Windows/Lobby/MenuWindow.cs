using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Exceptions;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Game;
using MafiCo.Console.Presentation.Windows.Lobby.Context;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class MenuWindow : Window{

    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        await AppComponents.GiveChoice(new () {
            {"Играть", async () => await SwitchTo<GameWindow>()},
            {"Настройки", async () => await SwitchTo<SettingsWindow>()},
            {"Выйти", () => throw new GameClosedException()}
        });
    }
}