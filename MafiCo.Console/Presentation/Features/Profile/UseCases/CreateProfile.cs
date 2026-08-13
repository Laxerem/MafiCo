using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Interfaces;
using DomainProfile = MafiCo.Domain.AggregatesModel.ProfileAggregate.Profile;

namespace MafiCo.Console.Presentation.Features.Profile.UseCases;

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
        var profile = _repository.Add(DomainProfile.Create(name));
        _store.SetUser(profile.Id);
        await _unitOfWork.SaveEntitiesAsync();
    }
}
