using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.Services;

public class GameService {
    private readonly ProfileService _profileService;
    private readonly BotService _botService;
    private readonly IUnitOfWork _unitOfWork;
    
    public GameService(ProfileService profileService, BotService botService, IUnitOfWork unitOfWork) {
        _profileService = profileService;
        _botService = botService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GameOrchestrator> Start() {
        var userProfile = await _profileService.GetMe();
        if (userProfile is null) throw new Exception("User not found");
        var bots = await _botService.GetAllAvailableBots();
        var botsIds = bots.Select(x => x.Id).ToList();

        List<Guid> profilesIds = [userProfile.Id, ..botsIds];

        var game = new Game();
        return new GameOrchestrator(game, profilesIds, _unitOfWork);
    }
}