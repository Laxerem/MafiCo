using MafiCo.Application.Interfaces;
using MafiCo.Application.Services;
using MafiCo.Console.App.Exceptions;
using MafiCo.Console.App.UI.Common;
using MafiCo.Console.App.UI.Events;
using MafiCo.Console.App.UI.Windows;
using MafiCo.Console.App.UI.Windows.Game;
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

    private void ChangeWindow(SwitchWindowEvent @event) {
        _currentWindow = @event.WindowType switch {
            var t when t == typeof(StartGameWindow) => new StartGameWindow(_gameService),
            _ => throw new Exception("Invalid window type")
        };
    }

    public async Task StartRetention() {
        AnsiConsole.Console.Clear();
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