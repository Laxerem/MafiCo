using Spectre.Console;

namespace MafiCo.Console.Presentation.Extensions;

public static class AppInterface {
    public static async Task MakeChoice(Dictionary<string, Func<Task>> choices) {
        var choicesText = choices.Keys.ToList();
        
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .AddChoices(choicesText));
        
        var func = choices[selected];
        await func.Invoke();
    }
}