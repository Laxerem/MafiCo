using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace MafiCo.Console.Presentation;

public class UserInterface {
    private Window _window;
    private readonly IServiceProvider _services;

    public UserInterface(IServiceProvider serviceProvider) {
        _services = serviceProvider;
    }
    private async Task ChangeWindow(SwitchWindowRequest evt) {
        ClearEventListeners();
        var newWindow = (Window)_services.GetRequiredService(evt.WindowType);
        _window = newWindow;
        SubscribeOnWindowEvents();
        await _window.Show();
    }

    private void SubscribeOnWindowEvents() {
        _window.OnSwitchWindow += ChangeWindow;
    }

    private void ClearEventListeners() {
        _window.OnSwitchWindow -= ChangeWindow;
    }

    public async Task StartRetention(Window window) {
        _window = window;
        SubscribeOnWindowEvents();
        try {
            AnsiConsole.Clear();
            await _window.Show();
        }
        catch (GameClosedException) {
            AnsiConsole.Clear();
            AnsiConsole.Console.Write(new Text("Мафия не ждёт...", new Style(new Color(255,0,0))));
        }
        catch (Exception ex) {
            AnsiConsole.WriteException(ex);
        }
    }
}
