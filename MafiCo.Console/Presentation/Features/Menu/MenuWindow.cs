using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Exceptions;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Game;
using MafiCo.Console.Presentation.Features.Profile;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Menu;

public class MenuWindow : Window{

    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        await AppComponents.GiveChoice(new () {
            {"Играть", async () => await SwitchTo<GameWindow>()},
            {"Профиль", async () => await SwitchTo<ProfileWindow>()},
            {"Настройки", async () => await SwitchTo<SettingsWindow>()},
            {"Выйти", () => throw new GameClosedException()}
        });
    }
}