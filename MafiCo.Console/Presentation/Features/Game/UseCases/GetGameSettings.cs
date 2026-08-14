using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Infrastructure.Services;
using MafiCo.Infrastructure.Services.Processors;

namespace MafiCo.Console.Presentation.Features.Game.UseCases;

public class GetGameSettings {
    private readonly GameService _gameService;
    private readonly ProfileService _profileService;

    public GetGameSettings(GameService gameService, ProfileService profileService) {
        _gameService = gameService;
        _profileService = profileService;
    }

    public async Task<SettingProcessor> ExecuteAsync() {
        var orchestrator = await _gameService.Start();
        var userInfo = await _profileService.GetMe();
        return orchestrator.GetSettingProcessor();
    }
}
