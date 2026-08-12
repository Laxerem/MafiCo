using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.SeedWork;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class InitialWindow : Window {
    public async override Task Show() {
        try {
            await RenderAsync();
        }
        catch (DomainException ex) {
            await RenderAsync(ex);
        }
        
        await Task.Delay(2000);
        await SwitchTo<MenuWindow>();
    }

    protected async Task RenderAsync(DomainException? error = null) {
        AnsiConsole.Clear();
        AnsiConsole.Console.Write(new FigletText("Hi!"));
        if (error == null) {
            AnsiConsole.Clear();
            var result = await AppComponents.GetUserInput("What is your name?");
            await UseAsync(new CreateProfile(result));
            AppComponents.WriteSuccess("Profile Created!");
        }
        else {
            AnsiConsole.Clear();
            AppComponents.WriteError($"Error: {error.Message}");
            var result = await AppComponents.GetUserInput("What is your name?");
            await UseAsync(new CreateProfile(result));
        }
    }
}