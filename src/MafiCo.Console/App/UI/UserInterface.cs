using MafiCo.Application.Services;
using MafiCo.Console.App.UI.Events;
using MafiCo.Domain.Exceptions;
using Spectre.Console;

namespace MafiCo.Console.App.UI;

public class UserInterface {
    private readonly GameService _gameService;
    
    public UserInterface(GameService gameService) {
        _gameService = gameService;
    }
    
    private async Task<string> WaitChoice(string title, IEnumerable<string> choices) {
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        return selected;
    }

    public async Task StartRetention() {
        try {
            while (true) {
                var choice = await ShowMenu();
                switch (choice) {
                    case GameStartEvent:
                        await _gameService.StartGameAsync();
                        break;
                    case GameClosedEvent:
                        AnsiConsole.WriteLine("Мафия не ждёт....");
                        return;
                }
            }
        }
        catch (GameException e) {
            AnsiConsole.WriteException(e);
            await Task.Delay(3000);
            AnsiConsole.Clear();
            await StartRetention();
        }
    }

    private async Task<UiEvent> ShowMenu() {
        var choice = await WaitChoice("MafiCo", ["Играть", "Выйти"]);
        switch (choice) {
            case "Играть":
                return new GameStartEvent();
            default:
                return new GameClosedEvent();
        }
    }
}