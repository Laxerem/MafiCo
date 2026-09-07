using MafiCo.Application.Interfaces.Stores;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastructure.MediatR.Profile.Commands;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Profile.Handlers;

public class GetMeHandler : IRequestHandler<GetMeCommand, ProfileInfo> {
    private readonly IProfileRepository _repository;
    private readonly IUserStore _store;

    public GetMeHandler(IProfileRepository repository, IUserStore store) {
        _repository = repository;
        _store = store;
    }

    public async Task<ProfileInfo> Handle(GetMeCommand request, CancellationToken cancellationToken) {
        var userId = _store.GetUserId() ?? throw new ProfileException("UserId can't be null");

        var profile = await _repository.GetAsync(userId)
            ?? throw new ProfileException("Profile not found");

        return new ProfileInfo(profile.Id, profile.Name);
    }
}
