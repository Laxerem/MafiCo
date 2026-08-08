using MafiCo.Console.App.UI.Events;
using MafiCo.Console.Presentation.Events;
using Spectre.Console;

namespace MafiCo.Console.Presentation;

public abstract class Window {
    public event Func<SwitchWindowEvent, Task>? OnSwitchWindow;
    public event Action<UiEvent>? OnEvent;
    
    protected async Task<string> WaitChoice(string title, IEnumerable<string> choices) {
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        return selected;
    }
    
    protected async Task SwitchTo<T>() where T : Window {
        AnsiConsole.Clear();
        await OnSwitchWindow?.Invoke(new SwitchWindowEvent(typeof(T)));
    }
    
    public abstract Task Show();
}