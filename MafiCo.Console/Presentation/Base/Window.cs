using System.Reflection;
using MafiCo.Domain.Interfaces;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Base;

public abstract class Window {
    public event Func<SwitchWindowRequest, Task>? OnSwitchWindow;
    public event Func<UseCase, Task>? OnEvent;
    
    protected async Task SwitchTo<T>() where T : Window {
        AnsiConsole.Clear();
        await OnSwitchWindow?.Invoke(new SwitchWindowRequest(typeof(T)))!;
    }

    protected async Task UseAsync(UseCase @case) {
        await OnEvent?.Invoke(@case)!;
    }
    
    public abstract Task Show();
}