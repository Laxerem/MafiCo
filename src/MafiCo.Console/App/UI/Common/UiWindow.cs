using MafiCo.Console.App.UI.Events;
using Spectre.Console;

namespace MafiCo.Console.App.UI.Common;

public abstract class UiWindow {
    public abstract event Action<UiWindow> OnSwitchWindow;
    protected abstract event Action<UiEvent> OnEvent;

    public UiWindow() {
        OnEvent += HandleEvent;
    }
    
    protected async Task<string> WaitChoice(string title, IEnumerable<string> choices) {
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        return selected;
    }
    
    public abstract Task Show();

    protected virtual void HandleEvent(UiEvent evt) {
        throw new NotImplementedException();
    }
}