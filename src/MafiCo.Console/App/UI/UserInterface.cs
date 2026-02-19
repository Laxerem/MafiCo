using MafiCo.Application.Interfaces;
using MafiCo.Application.Services;
using MafiCo.Console.App.Exceptions;
using MafiCo.Console.App.UI.Common;
using MafiCo.Console.App.UI.Events;
using MafiCo.Console.App.UI.Windows;
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
    private UiWindow _currentWindow;
    
    public UserInterface(GameService gameService, HumanBrain viewerBrain) {
        _gameService = gameService;
        _viewer = viewerBrain;
        _currentWindow = new MenuWindow();
        _currentWindow.OnSwitchWindow += ChangeWindow;
    }

    private void ChangeWindow(UiWindow window) {
        _currentWindow = window;
    }

    private async Task ProcessEvent(IDomainEvent domainEvent) {
        AnsiConsole.Clear();
        switch (domainEvent) {
            case RoleDeterminateEvent:
                var evt = domainEvent as RoleDeterminateEvent;
                if (evt.Role is Role.Citizen) {
                    AnsiConsole.MarkupLine($"Твоя роль:[green] {evt.Role}[/]");   
                }
                else {
                    AnsiConsole.MarkupLine($"Твоя роль:[red] {evt.Role}[/]");  
                }
                await Task.Delay(4000);
                AnsiConsole.Clear();
                break;
        }
    }

    public async Task StartRetention() {
        try {
            while (true) {
                await _currentWindow.Show();
            }
        }
        catch (GameException e) {
            AnsiConsole.WriteException(e);
            await Task.Delay(3000);
            AnsiConsole.Clear();
            await StartRetention();
        }
        catch (GameClosedException e) {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine(e.Message);
        }
    }
}