using MafiCo.Console.Presentation.Events;
using Spectre.Console;

namespace MafiCo.Console.Presentation;

public class UserInterface {
    private Window _window;

    public UserInterface(Window startWindow) {
        _window = startWindow;
        _window.OnSwitchWindow += ChangeWindow;
    }
    private async Task ChangeWindow(SwitchWindowEvent evt) {
        _window.OnSwitchWindow -= ChangeWindow;
        _window = (Window)Activator.CreateInstance(evt.WindowType)!;
        _window.OnSwitchWindow += ChangeWindow;
        await _window.Show();
    }

    public async Task StartRetention() {
        try {
            await _window.Show();
        }
        catch (Exception ex) {
            AnsiConsole.WriteException(ex);
        }
    }
}