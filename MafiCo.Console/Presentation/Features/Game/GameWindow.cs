using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Infrastructure.MediatR.Game.Commands;
using MafiCo.Infrastructure.MediatR.Profile.Commands;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game;

public class GameWindow : Window {
    private readonly IMediator _mediator;

    public GameWindow(IMediator mediator) {
        _mediator = mediator;
    }

    public override async Task Show() {
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        var userInput = await AppComponents.GetUserInput("Количество мафии: ");
        var mafiaCount = int.Parse(userInput);

        var me = await _mediator.Send(new GetMeCommand());
        var playerContext = await _mediator.Send(new StartGameCommand(mafiaCount));
        await new GameSession(playerContext, me.Name).RunAsync();
    }
}
