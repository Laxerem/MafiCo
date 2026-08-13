using Spectre.Console;

namespace MafiCo.Console.Presentation.Extensions;

public static class AppComponents {
    public static async Task GiveChoice(Dictionary<string, Func<Task>> choices) {
        var choicesText = choices.Keys.ToList();
        
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .AddChoices(choicesText));
        
        var func = choices[selected];
        await func.Invoke();
    }

    public static async Task<string> GetUserInput(string title) {
        return await AnsiConsole.PromptAsync(new TextPrompt<string>(title));
    }

    public static async Task<T> SelectFrom<T>(string title, List<T> items, Func<T, string> converter) where T : notnull {
        return await AnsiConsole.PromptAsync(
            new SelectionPrompt<T>()
                .Title(title)
                .UseConverter(converter)
                .AddChoices(items));
    }

    public static void WriteSuccess(string text) {
        AnsiConsole.Console.Write(new Text(text, new Style(new Color(0, 255, 0))));
    }

    public static void WriteError(string text) {
        AnsiConsole.Console.Write(new Text(text, new Style(new Color(255, 0, 0))));
    }

    public static void WriteInfo(string text) {
        AnsiConsole.Console.Write(new Text(text, new Style(new Color(255, 255, 0))));
    }
}