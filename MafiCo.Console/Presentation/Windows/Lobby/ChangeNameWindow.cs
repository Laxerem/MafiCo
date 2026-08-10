using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.Requests;
using MafiCo.Domain.Events.Profile;
using MafiCo.Domain.Interfaces;
using MafiCo.Infrastructure.Handlers;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class ChangeNameWindow : Window {
    public async override Task Show() {
        var result = await AppInterface.GetUserInput("Новое имя");
        await SendRequest(new ChangeNameRequest(result));
    }

    public async override Task HandleEvent(InterfaceEvent evt) {
        if (evt is UINameChangedEvent) {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Text("Имя успешно изменено", new Style(new Color(0, 255, 0))));
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}