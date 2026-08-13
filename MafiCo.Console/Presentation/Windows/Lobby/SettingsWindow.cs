using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class SettingsWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Settings"));
        await AppComponents.GiveChoice(new() {
                {"Настройки ботов", async () => await SwitchTo<BotSettingsWindow>()},
                {"Настройки ИИ моделей", async () => await SwitchTo<LlmSettingsWindow>()},
                {"Изменить имя", async () => await SwitchTo<ChangeNameWindow>()},
                {"Назад", async () => await SwitchTo<MenuWindow>()}
            }
        );
    }
}