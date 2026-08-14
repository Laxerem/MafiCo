using MafiCo.Console.Presentation;
using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Features.Menu;
using MafiCo.Console.Presentation.Features.Profile;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Interfaces.Store;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Console;

public class App {
    private readonly IServiceProvider _services;
    private readonly IProfileRepository _profileRepository;
    private readonly IUserStore _store;
    
    public App(IServiceProvider serviceProvider, IProfileRepository profileRepository, IUserStore appStore) {
        _services = serviceProvider;
        _profileRepository = profileRepository;
        _store = appStore;
    }

    public async Task RunAsync() {
        Window? initialWindow = null;
        var userId = _store.GetUserId();
        if (userId == null) {
            initialWindow = _services.GetRequiredService<InitialWindow>();
        }
        else {
            var userProfile = await _profileRepository.GetAsync(userId.Value);
            if (userProfile == null) {
                throw new Exception($"Profile with id {userId.Value} not found");
            }
            initialWindow = new MenuWindow();
        }
        var appInterface = _services.GetRequiredService<UserInterface>();
        await appInterface.StartRetention(initialWindow!);
    }
}