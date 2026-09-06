using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Infrastructure.MediatR.Game.Commands;
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
        int mafiaCount = int.Parse(userInput);
        
        var playerContext = await _mediator.Send(new StartGameCommand(mafiaCount));
        playerContext.OnNotification += OnNotification;
    }

    private void OnNotification(INotification notification) {
        AnsiConsole.Console.Write(NotificationBuilder.Build(notification));
    }
}
