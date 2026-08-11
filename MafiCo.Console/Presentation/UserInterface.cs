using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace MafiCo.Console.Presentation;

public class UserInterface {
    private Window _window;
    private readonly IMediator _mediator;
    private readonly IServiceProvider _services;

    public UserInterface(IMediator mediator, IServiceProvider serviceProvider) {
        _mediator = mediator;
        _services = serviceProvider;
    }
    private async Task ChangeWindow(SwitchWindowRequest evt) {
        ClearEventListeners();
        var newWindow = (Window)_services.GetRequiredService(evt.WindowType);
        _window = newWindow;
        SubscribeOnWindowEvents();
        await _window.Show();
    }

    private async Task OnWindowEvent(UseCase evt) {
        await _mediator.Publish(evt);
    }

    private void SubscribeOnWindowEvents() {
        _window.OnSwitchWindow += ChangeWindow;
        _window.OnEvent += OnWindowEvent;
    }

    private void ClearEventListeners() {
        _window.OnSwitchWindow -= ChangeWindow;
        _window.OnEvent -= OnWindowEvent;
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