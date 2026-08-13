using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class ChangeNameWindow : Window {
    private readonly ChangeName _changeName;

    public ChangeNameWindow(ChangeName changeName) {
        _changeName = changeName;
    }

    public async override Task Show() {
        var result = await AppComponents.GetUserInput("Новое имя");
        await _changeName.ExecuteAsync(result);
        AppComponents.WriteSuccess("Имя изменено!");
        await Task.Delay(2000);
        await SwitchTo<SettingsWindow>();
    }
}
