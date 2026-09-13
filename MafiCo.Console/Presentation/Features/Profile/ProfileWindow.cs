using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Menu;
using MafiCo.Application.Profile.Commands;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Profile;

public class ProfileWindow : Window {
    private readonly IMediator _mediator;

    public ProfileWindow(IMediator mediator) {
        _mediator = mediator;
    }

    public override async Task Show() {
        var profile = await _mediator.Send(new GetMeCommand());

        AnsiConsole.Console.Write(new FigletText("Profile"));

        var table = new Table()
            .AddColumn("Имя")
            .AddColumn("Победы")
            .AddColumn("Поражения");
        table.AddRow(profile.Name, profile.VictoriesCount.ToString(), profile.DefeatsCount.ToString());
        AnsiConsole.Write(table);

        await AppComponents.GiveChoice(new() {
            {"Изменить имя", async () => await SwitchTo<ChangeNameWindow>()},
            {"Назад", async () => await SwitchTo<MenuWindow>()}
        });
    }
}
