using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Game.UseCases;
using MafiCo.Infrastructure.Services.Processors;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game;

public class GameSettingWindow : Window {
    private readonly GetGameSettings _getGameSettings;

    public GameSettingWindow(GetGameSettings getGameSettings) {
        _getGameSettings = getGameSettings;
    }

    public async override Task Show() {
        var processor = await _getGameSettings.ExecuteAsync();
        
        AnsiConsole.Console.Write(new FigletText("MafiCo"));
        var mafiaCount = int.Parse(await AppComponents.GetUserInput("Количество мафии"));
        processor.SetupMafiaCount(mafiaCount);
        await processor.StartGame();
        await SwitchTo<GameWindow>();
    }
}
