using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.Events.Profile;
using MafiCo.Infrastructure.Interfaces;
using MediatR;

namespace MafiCo.Console.Handlers;

// TODO: Move to infrastructure
public class CreateProfileHandler : INotificationHandler<CreateProfile> {
    private readonly IProfileRepository _repository;
    private readonly IAppStore _store;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateProfileHandler(IProfileRepository repository, IAppStore store, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _store = store;
    }
    
    public async Task Handle(CreateProfile usecase, CancellationToken cancellationToken) {
        var profile = _repository.Add(Profile.Create(usecase.Name));
        _store.SetUser(profile.Id);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}