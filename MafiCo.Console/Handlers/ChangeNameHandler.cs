using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Services;
using MediatR;

namespace MafiCo.Console.Handlers;

//TODO: Move to infrastructure
public class ChangeNameHandler : INotificationHandler<ChangeName> {
    private readonly ProfileService _profileService;
    
    public ChangeNameHandler(ProfileService profileService) {
        _profileService = profileService;
    }

    public async Task Handle(ChangeName notification, CancellationToken cancellationToken) {
        await _profileService.ChangeNameAsync(notification.Name);
    }
}