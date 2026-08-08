using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class SettingsWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Settings"));
        await AppInterface.GiveChoice(new() {
                {"Создать бота", async () => await SwitchTo<LlmBotCreatingWindow>()},
                {"Назад", async () => await SwitchTo<MenuWindow>()}
            }
        );
    }
}