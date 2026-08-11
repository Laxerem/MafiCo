using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Console.Handlers;

//TODO Refactor and move to another layer
public class CreateProfileHandler : INotificationHandler<CreateProfile> {
    private readonly IProfileRepository _repository;
    private readonly ConfigurationController _configuration;
    
    public CreateProfileHandler(IProfileRepository repository, ConfigurationController configuration) {
        _repository = repository;
        _configuration = configuration;
    }
    
    public Task Handle(CreateProfile notification, CancellationToken cancellationToken) {
        var profile = _repository.Add(Profile.Create(notification.Name));
        _configuration.UpdateUser(new UserOptions() {Id = profile.Id});
        return Task.CompletedTask;
    }
}