using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.Events.Profile;
using MafiCo.Domain.Interfaces;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class ChangeNameWindow : Window {
    public async override Task Show() {
        var result = await AppComponents.GetUserInput("Новое имя");
        await UseAsync(new ChangeName(result));
        AppComponents.WriteSuccess("Имя изменено!");
        await Task.Delay(2000);
        await SwitchTo<SettingsWindow>();
    }
}