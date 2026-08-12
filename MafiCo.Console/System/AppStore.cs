using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Console.System;

public class AppStore : IAppStore {
    private readonly ConfigurationController _config;
    
    public AppStore(ConfigurationController config) {
        _config = config;
    }
    
    public void SetUser(Guid userId) {
        _config.SetUserOptions(new UserOptions() {Id = userId});
    }

    public Guid? GetUserId() {
        return _config.GetUserOptions().Id;
    }
}