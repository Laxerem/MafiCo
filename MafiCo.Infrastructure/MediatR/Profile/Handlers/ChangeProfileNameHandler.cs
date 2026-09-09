using MafiCo.Application.Interfaces.Stores;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.MediatR.Profile.Commands;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Profile.Handlers;

public class ChangeProfileNameHandler : IRequestHandler<ChangeProfileNameCommand> {
    private readonly IProfileRepository _repository;
    private readonly IUserStore _store;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeProfileNameHandler(IProfileRepository repository, IUserStore store, IUnitOfWork unitOfWork) {
        _repository = repository;
        _store = store;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeProfileNameCommand request, CancellationToken cancellationToken) {
        var userId = _store.GetUserId() ?? throw new ProfileException("UserId can't be null");

        var profile = await _repository.GetAsync(userId)
            ?? throw new ProfileException("Profile not found");

        profile.ChangeName(request.Name);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
