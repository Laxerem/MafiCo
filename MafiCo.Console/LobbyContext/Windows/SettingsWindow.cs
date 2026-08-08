using MafiCo.Console.Presentation;
using Spectre.Console;

namespace MafiCo.Console.LobbyContext.Windows;

public class SettingsWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Settings"));
        var choice = await WaitChoice("", ["Создать бота", "Назад"]);
        switch (choice) {
            case "Создать бота":
                throw new Exception("Мафия не ждёт...");
                break;
            case "Назад":
                await SwitchTo<MenuWindow>();
                break;
        }
    }
}