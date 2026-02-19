using MafiCo.Console.App.UI.Events;
using Spectre.Console;

namespace MafiCo.Console.App.UI.Common;

public abstract class UiWindow {
    public event Action<SwitchWindowEvent>? OnSwitchWindow;
    public event Action<UiEvent>? OnEvent;
    
    protected async Task<string> WaitChoice(string title, IEnumerable<string> choices) {
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        return selected;
    }
    
    public abstract Task Show();
    protected void SwitchTo<T>() where T : UiWindow {
        OnSwitchWindow?.Invoke(new SwitchWindowEvent(typeof(T)));
    }

    protected void RaiseEvent(UiEvent evt) {
        OnEvent?.Invoke(evt);
        HandleEvent(evt);
    }

    protected virtual void HandleEvent(UiEvent evt) {
        throw new NotImplementedException();
    }
}