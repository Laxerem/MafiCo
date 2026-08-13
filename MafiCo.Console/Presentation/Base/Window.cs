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

    /// <summary>
    /// Выполняет действие, показывает результат (успех/ошибку) и возвращает пользователя на окно T.
    /// </summary>
    protected async Task RunAndReturnAsync<T>(Func<Task> action, string successMessage, string errorPrefix) where T : Window {
        try {
            await action();
            AppComponents.WriteSuccess(successMessage);
        }
        catch (Exception ex) {
            AppComponents.WriteError($"{errorPrefix}: {ex.Message}");
        }

        await Task.Delay(2000);
        await SwitchTo<T>();
    }

    public abstract Task Show();
}