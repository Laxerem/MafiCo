using MafiCo.Console.Presentation.Events;
using MafiCo.Console.Presentation.Events.Common;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation;

public class UserInterface {
    private Window _window;
    private readonly IMediator _mediator;

    public UserInterface(Window startWindow) {
        _window = startWindow;
        SubscribeOnWindowEvents(startWindow);
    }
    private async Task ChangeWindow(SwitchWindowEvent evt) {
        ClearEventListeners();
        var newWindow = (Window)Activator.CreateInstance(evt.WindowType)!;
        SubscribeOnWindowEvents(newWindow);
        await _window.Show();
    }

    private async Task OnWindowEvent(UiEvent evt) {
        await _mediator.Publish(evt);
    }

    private void SubscribeOnWindowEvents(Window window) {
        _window = window;
        _window.OnSwitchWindow += ChangeWindow;
        _window.OnEvent += OnWindowEvent;
    }

    private void ClearEventListeners() {
        _window.OnSwitchWindow -= ChangeWindow;
        _window.OnEvent -= OnWindowEvent;
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