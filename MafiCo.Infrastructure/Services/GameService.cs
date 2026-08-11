using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Infrastructure.Services;

public class GameService {
    private readonly IProfileRepository _profileRepository;
    
    public GameService(IProfileRepository repository) {
        _profileRepository = repository;
    }

    public async Task Start() {
        var game = new Game();
        var profiles = await _profileRepository.GetAllAsync();
    }
}