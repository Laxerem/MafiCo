using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class InitialWindow : Window {
    public async override Task Show() {
        AnsiConsole.Console.Write(new FigletText("Hi!"));
        var result = await AppComponents.GetUserInput("What is your name?");
        await UseAsync(new CreateProfile(result));
        AnsiConsole.Clear();
        AnsiConsole.Console.Write(new Text("Profile created!", new Style(new Color(0, 255, 0))));
        await Task.Delay(2000);
        await SwitchTo<MenuWindow>();
    }
}