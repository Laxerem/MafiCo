using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Stores;
using MafiCo.Application.Profile.Commands;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;
using DomainProfile = MafiCo.Domain.AggregatesModel.ProfileAggregate.Profile;

namespace MafiCo.Application.Profile.Handlers;

public class CreateProfileHandler : IRequestHandler<CreateProfileCommand> {
    private readonly IProfileRepository _repository;
    private readonly IUserStore _store;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProfileHandler(IProfileRepository repository, IUserStore store, IUnitOfWork unitOfWork) {
        _repository = repository;
        _store = store;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateProfileCommand request, CancellationToken cancellationToken) {
        var profile = _repository.Add(DomainProfile.Create(request.Name));
        _store.SetUser(profile.Id);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
