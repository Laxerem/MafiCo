using System.Reflection;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Domain.Interfaces;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Base;

public abstract class Window {
    public event Func<SwitchWindowRequest, Task>? OnSwitchWindow;

    protected async Task SwitchTo<T>() where T : Window {
        AnsiConsole.Clear();
        await OnSwitchWindow?.Invoke(new SwitchWindowRequest(typeof(T)))!;
    }

    public abstract Task Show();
}