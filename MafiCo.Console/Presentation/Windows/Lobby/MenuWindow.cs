using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Exceptions;
using MafiCo.Console.Presentation.Extensions;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class MenuWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        await AppInterface.GiveChoice(new () {
            {"Играть", () => throw new Exception("Мафия не ждёт..")},
            {"Настройки", async () => await SwitchTo<SettingsWindow>()},
            {"Выйти", () => throw new GameClosedException()}
        });
    }
}