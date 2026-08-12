using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class SettingsWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Settings"));
        await AppComponents.GiveChoice(new() {
                // {"Настройки ботов", async () => }
                {"Добавить ИИ модель", async () => await SwitchTo<LlmAddingWindow>()},
                {"Изменить имя", async () => await SwitchTo<ChangeNameWindow>()},
                {"Назад", async () => await SwitchTo<MenuWindow>()}
            }
        );
    }
}