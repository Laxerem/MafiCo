using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Interfaces.Stores;

namespace MafiCo.Infrastructure.Services;

public class GameService {
    private readonly ProfileService _profileService;
    private readonly IGameRepository _gameRepository;
    private readonly BotService _botService;
    private readonly IGameStore _gameStore;
    private readonly IUnitOfWork _unitOfWork;
    
    public GameService(ProfileService profileService, IGameRepository gameRepository, BotService botService, IGameStore gameStore, 
        IUnitOfWork unitOfWork) {
        _profileService = profileService;
        _gameRepository = gameRepository;
        _botService = botService;
        _gameStore = gameStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<GameOrchestrator> Start() {
        var userProfile = await _profileService.GetMe();
        if (userProfile is null) throw new Exception("User not found");
        var bots = await _botService.GetAllAvailableBots();
        var botsIds = bots.Select(x => x.Id).ToList();

        List<Guid> profilesIds = [userProfile.Id, ..botsIds];

        var game = new Game();
        var orchestrator = new GameOrchestrator(game, profilesIds, _unitOfWork);
        _gameStore.SetGame(orchestrator);
        _gameRepository.Add(game);
        return orchestrator;
    }
}