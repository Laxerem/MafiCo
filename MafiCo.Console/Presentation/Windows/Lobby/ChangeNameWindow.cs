using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.Requests;
using MafiCo.Domain.Events.Profile;
using MafiCo.Domain.Interfaces;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class ChangeNameWindow : Window {
    public async override Task Show() {
        var result = await AppComponents.GetUserInput("Новое имя");
        await UseAsync(new ChangeName(result));
    }
}