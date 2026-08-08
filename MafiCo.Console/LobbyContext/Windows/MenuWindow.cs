using MafiCo.Console.Presentation;
using Spectre.Console;

namespace MafiCo.Console.LobbyContext.Windows;

public class MenuWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        var choice = await WaitChoice("", ["Играть", "Настройки", "Выйти"]);
        switch (choice) {
            case "Играть":
                throw new Exception("Мафия не ждёт...");
                break;
            case "Настройки":
                SwitchTo<SettingsWindow>();
                break;
            case "Выйти":
                throw new Exception("Мафия не ждёт...");
        }
    }
}