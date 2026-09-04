using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Menu;
using MafiCo.Infrastructure.MediatR.Profile.Commands;
using MediatR;

namespace MafiCo.Console.Presentation.Features.Profile;

public class ChangeNameWindow : Window {
    private readonly IMediator _mediator;

    public ChangeNameWindow(IMediator mediator) {
        _mediator = mediator;
    }

    public async override Task Show() {
        var newName = await AppComponents.GetUserInput("Новое имя");

        await RunAndReturnAsync<SettingsWindow>(
            () => _mediator.Send(new ChangeProfileNameCommand(newName)),
            "Имя изменено!",
            "Не удалось изменить имя");
    }
}
