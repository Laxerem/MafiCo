using MafiCo.Application.Database.Repositories;
using MafiCo.Application.Exceptions;
using MafiCo.Domain.Entities;
using MafiCo.Domain.Exceptions;

namespace MafiCo.Application.Database;

public class GameMemory {
    private readonly string _filePath;
    private readonly HumanProfilesRepository _humanProfiles;
    private readonly BotProfilesRepository _botProfiles;
    
    public GameMemory(string filePath) {
        _filePath = filePath;
        _humanProfiles = new HumanProfilesRepository(filePath);
        _botProfiles = new BotProfilesRepository(filePath);
    }

    public async Task InitializeAsync() {
        await _humanProfiles.InitializeTableAsync();
        await _botProfiles.InitializeTableAsync();
    }

    public async Task<HumanProfile?> GetHumanProfileAsync() {
        var profiles = await _humanProfiles.GetAllAsync();
        var result = profiles.FirstOrDefault();
        return result;
    }

    public async Task<List<BotProfile>> GetBotProfilesAsync() {
        var profiles = await _botProfiles.GetAllAsync();
        var result = profiles.ToList();
        return result;
    }
}