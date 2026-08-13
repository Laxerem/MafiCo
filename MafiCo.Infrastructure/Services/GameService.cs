using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Infrastructure.Services.Controllers;

namespace MafiCo.Infrastructure.Services;

public class GameService {
    private readonly ProfileService _profileService;
    private readonly BotService _botService;
    
    public GameService(ProfileService profileService,  BotService botService) {
        _profileService = profileService;
        _botService = botService;
    }

    public async Task<PlayerController> Start() {
        var userProfile = await _profileService.GetMe();
        if (userProfile is null) throw new Exception("User not found");
        var bots = await _botService.GetAllAvailableBots();
        var botsIds = bots.Select(x => x.Id).ToList();

        List<Guid> profilesIds = [userProfile.Id, ..botsIds];

        var game = new Game();
        game.Setup(profilesIds, 1);
        
        return new PlayerController(userProfile.Id, game);
    }
}