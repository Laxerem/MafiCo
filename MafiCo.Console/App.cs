using MafiCo.Console.Configuration.Options;
using MafiCo.Console.Presentation;
using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Windows.Lobby;
using MafiCo.Console.Presentation.Windows.Lobby.Context;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MafiCo.Console;

public class App {
    private readonly IServiceProvider _services;
    private readonly IProfileRepository _profileRepository;
    private readonly UserOptions _options;
    
    public App(IServiceProvider serviceProvider, IProfileRepository profileRepository, IOptions<UserOptions> options) {
        _services = serviceProvider;
        _profileRepository = profileRepository;
        _options = options.Value;
    }

    public async Task RunAsync() {
        Window? initialWindow = null;
        if (_options.Id == null) {
            initialWindow = _services.GetRequiredService<InitialWindow>();
        }
        else {
            var userProfile = await _profileRepository.GetAsync(_options.Id.Value);
            if (userProfile == null) {
                throw new Exception($"Profile with id {_options.Id} not found");
            }
            initialWindow = new MenuWindow();
        }
        var appInterface = _services.GetRequiredService<UserInterface>();
        await appInterface.StartRetention(initialWindow!);
    }
}