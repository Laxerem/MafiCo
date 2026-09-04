using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Menu;
using MafiCo.Domain.SeedWork;
using MafiCo.Infrastructure.MediatR.Profile.Commands;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Profile;

public class InitialWindow : Window {
    private readonly IMediator _mediator;

    public InitialWindow(IMediator mediator) {
        _mediator = mediator;
    }

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
            await _mediator.Send(new CreateProfileCommand(result));
            AppComponents.WriteSuccess("Profile Created!");
        }
        else {
            AnsiConsole.Clear();
            AppComponents.WriteError($"Error: {error.Message}");
            var result = await AppComponents.GetUserInput("What is your name?");
            await _mediator.Send(new CreateProfileCommand(result));
        }
    }
}
