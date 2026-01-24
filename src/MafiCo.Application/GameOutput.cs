using Spectre.Console;

namespace MafiCo.Application;

public class GameOutput {
    public GameOutput() {
        
    }

    public void Show(string text) {
        Console.WriteLine(text);
    }

    public string WaitChoice(string title, IEnumerable<string> choices) {
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        return selected;
    }
}