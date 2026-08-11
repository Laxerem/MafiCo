using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.SeedWork;
using MediatR;

namespace MafiCo.Console.Handlers;

//TODO Refactor and move to another layer
public class CreateProfileHandler : INotificationHandler<CreateProfile> {
    private readonly IProfileRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ConfigurationController _configuration;
    
    public CreateProfileHandler(IProfileRepository repository, IUnitOfWork unitOfWork, ConfigurationController configuration) {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    
    public async Task Handle(CreateProfile notification, CancellationToken cancellationToken) {
        var profile = _repository.Add(Profile.Create(notification.Name));
        await _configuration.UpdateUserAsync(new UserOptions() {Id = profile.Id});
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}