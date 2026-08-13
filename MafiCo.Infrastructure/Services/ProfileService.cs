using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.SeedWork;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.Services;

public class ProfileService {
    private readonly IAppStore _store;
    private readonly IProfileRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileService(IAppStore store, IProfileRepository repository, IUnitOfWork unitOfWork) {
        _store = store;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProfileInfo?> GetMe() {
        var profile = await _repository.GetAsync(_store.GetUserId()!.Value);
        if (profile is null) return null;
        return profile.ToProfileInfo();
    }
    
    public async Task ChangeNameAsync(string newName) {
        var userId = _store.GetUserId() ?? throw new ProfileException("UserId can't be null");
        var profile = await _repository.GetAsync(userId);
        if (profile is null) {
            throw new ProfileException("Profile not found");
        }
        profile.ChangeName(newName);
        await _unitOfWork.SaveEntitiesAsync();
    }
}