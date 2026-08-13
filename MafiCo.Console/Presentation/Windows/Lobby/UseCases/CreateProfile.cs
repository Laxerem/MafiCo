using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

// TODO: Move to infrastructure
public class CreateProfile {
    private readonly IProfileRepository _repository;
    private readonly IAppStore _store;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProfile(IProfileRepository repository, IAppStore store, IUnitOfWork unitOfWork) {
        _repository = repository;
        _store = store;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(string name) {
        var profile = _repository.Add(Profile.Create(name));
        _store.SetUser(profile.Id);
        await _unitOfWork.SaveEntitiesAsync();
    }
}
