using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Infrastructure.Interfaces.Store;

namespace MafiCo.Console.System.Stores;

public class UserStore : IUserStore {
    private readonly ConfigurationController _config;
    
    public UserStore(ConfigurationController config) {
        _config = config;
    }
    
    public void SetUser(Guid userId) {
        _config.SetUserOptions(new UserOptions() {Id = userId});
    }

    public Guid? GetUserId() {
        return _config.GetUserOptions().Id;
    }
}