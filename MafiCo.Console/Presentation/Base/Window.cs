using System.Reflection;
using MafiCo.Domain.Interfaces;
using MafiCo.Infrastructure.Handlers;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Base;

public abstract class Window {
    public event Func<SwitchWindowRequest, Task>? OnSwitchWindow;
    public event Func<UiRequest, Task>? OnEvent;
    
    protected async Task SwitchTo<T>() where T : Window {
        AnsiConsole.Clear();
        await OnSwitchWindow?.Invoke(new SwitchWindowRequest(typeof(T)))!;
    }

    protected async Task SendRequest(UiRequest evt) {
        await OnEvent?.Invoke(evt)!;
    }

    public virtual Task HandleEvent(InterfaceEvent evt) {
        return Task.CompletedTask;
    }
    
    public abstract Task Show();
}