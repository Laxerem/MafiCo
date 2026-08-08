using MafiCo.Console.Presentation.Events;
using MafiCo.Console.Presentation.Events.Common;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Base;

public abstract class Window {
    public event Func<SwitchWindowEvent, Task>? OnSwitchWindow;
    public event Func<UiEvent, Task>? OnEvent;
    
    protected async Task SwitchTo<T>() where T : Window {
        AnsiConsole.Clear();
        await OnSwitchWindow?.Invoke(new SwitchWindowEvent(typeof(T)));
    }

    protected async Task RaiseEvent(UiEvent evt) {
        await OnEvent?.Invoke(evt)!;
    }
    
    public abstract Task Show();
}