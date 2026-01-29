using MafiCo.Application.Interfaces;
using MafiCo.Application.Services;
using MafiCo.Console.App.UI.Events;
using MafiCo.Domain.Events;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;
using MafiCo.Infrastracture.Brains;
using Spectre.Console;

namespace MafiCo.Console.App.UI;

public class UserInterface {
    private readonly GameService _gameService;
    private readonly HumanBrain _viewer;
    
    public UserInterface(GameService gameService, HumanBrain viewerBrain) {
        _gameService = gameService;
        _viewer = viewerBrain;
    }
    
    private async Task<string> WaitChoice(string title, IEnumerable<string> choices) {
        var selected = await AnsiConsole.PromptAsync(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        return selected;
    }

    private async Task ProcessEvent(IGameEvent gameEvent) {
        AnsiConsole.Clear();
        switch (gameEvent) {
            case RoleIsDetermineEvent:
                var evt = gameEvent as RoleIsDetermineEvent;
                if (evt.Role is Role.Citizen) {
                    AnsiConsole.MarkupLine($"Твоя роль:[green] {evt.Role}[/]");   
                }
                else {
                    AnsiConsole.MarkupLine($"Твоя роль:[red] {evt.Role}[/]");  
                }
                await Task.Delay(4000);
                break;
        }
    }

    public async Task StartRetention() {
        try {
            while (true) {
                var choice = await ShowMenu();
                switch (choice) {
                    case GameStartEvent:
                        _viewer.Actions += ProcessEvent;
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

    private async Task InputResult() {
        
    }
}