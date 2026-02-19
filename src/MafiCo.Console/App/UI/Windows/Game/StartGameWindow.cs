using MafiCo.Console.App.UI.Common;
using MafiCo.Console.App.UI.Events;
using Spectre.Console;

namespace MafiCo.Console.App.UI.Windows.Game;

public class StartGameWindow :  UiWindow {
    public override event Action<UiWindow> OnSwitchWindow;
    protected override event Action<UiEvent> OnEvent;
    public override async Task Show() {
        AnsiConsole.WriteLine("");
    }
}