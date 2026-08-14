using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Infrastructure.Services;
using MafiCo.Infrastructure.Services.Processors;

namespace MafiCo.Console.Presentation.Features.Game.UseCases;

public class GetGameSettings {
    private readonly GameService _gameService;

    public GetGameSettings(GameService gameService) {
        _gameService = gameService;
    }

    public async Task<SettingProcessor> ExecuteAsync() {
        var orchestrator = await _gameService.Start();
        return orchestrator.GetSettingProcessor();
    }
}
