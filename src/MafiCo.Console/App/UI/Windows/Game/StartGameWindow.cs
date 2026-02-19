using MafiCo.Application.Services;
using MafiCo.Console.App.UI.Common;
using MafiCo.Console.App.UI.Events;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace MafiCo.Console.App.UI.Windows.Game;

public class StartGameWindow :  UiWindow {
    private readonly GameService _gameService;

    public StartGameWindow(GameService gameService) {
        _gameService = gameService;
    }
    
    public override async Task Show() {
        RaiseEvent(new GameStartEvent());
        await _gameService.StartGameAsync();
        await Task.Delay(Timeout.Infinite);
    }
    protected override void HandleEvent(UiEvent evt) {
        AnsiConsole.Console.Write(new FigletText("MafiCo.Console"));
    }
}