using MafiCo.Console.Presentation;
using Spectre.Console;

namespace MafiCo.Console.LobbyContext.Windows;

public class SettingsWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Settings"));
        
    }
}