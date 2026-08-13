using MafiCo.Infrastructure.Services;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

// TODO: Move to infrastructure
public class ChangeName {
    private readonly ProfileService _profileService;

    public ChangeName(ProfileService profileService) {
        _profileService = profileService;
    }

    public async Task ExecuteAsync(string name) {
        await _profileService.ChangeNameAsync(name);
    }
}
