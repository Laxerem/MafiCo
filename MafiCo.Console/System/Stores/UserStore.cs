using MafiCo.Application.Interfaces.Stores;
using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Console.System.Stores;

public class UserStore : IUserStore {
    private readonly ConfigurationController _config;

    public UserStore(ConfigurationController config) {
        _config = config;
    }

    public void SetUser(ProfileId userId) {
        _config.SetUserOptions(new UserOptions() {Id = userId.Id});
    }

    public ProfileId? GetUserId() {
        var id = _config.GetUserOptions().Id;
        return id.HasValue ? new ProfileId(id.Value) : null;
    }
}