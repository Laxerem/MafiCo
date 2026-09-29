using MafiCo.Domain.DTOs;

namespace MafiCo.Domain.AggregatesModel.ProfileAggregate;

public interface IProfileRepository {
    Profile Add(Profile profile);
    void Update(Profile profile);
    bool Exists(ProfileId id);
    Task<Profile?> GetAsync(ProfileId id);
    Task<List<Profile>> GetRangeAsync(IEnumerable<ProfileId> playerIds);
    Task<List<Profile>> GetAllAsync();
}
