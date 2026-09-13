using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Bots;
using MafiCo.Console.Presentation.Features.Llm;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Menu;

public class SettingsWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Settings"));
        await AppComponents.GiveChoice(new() {
                {"Настройки ботов", async () => await SwitchTo<BotSettingsWindow>()},
                {"Настройки ИИ моделей", async () => await SwitchTo<LlmSettingsWindow>()},
                {"Назад", async () => await SwitchTo<MenuWindow>()}
            }
        );
    }
}