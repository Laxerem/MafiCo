using MafiCo.Console.Presentation.Events;
using Spectre.Console;

namespace MafiCo.Console.Presentation;

public class UserInterface {
    private Window _window;

    public UserInterface(Window startWindow) {
        _window = startWindow;
        _window.OnSwitchWindow += ChangeWindow;
    }
    private void ChangeWindow(SwitchWindowEvent evt) {
        Type type = evt.WindowType;
        object instance = Activator.CreateInstance(type);
        _window = instance as Window ?? throw new InvalidOperationException();
        _window.Show();
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