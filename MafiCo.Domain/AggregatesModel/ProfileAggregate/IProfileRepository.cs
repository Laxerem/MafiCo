using MafiCo.Domain.DTOs;

namespace MafiCo.Domain.AggregatesModel.ProfileAggregate;

public interface IProfileRepository {
    Profile Add(Profile profile);
    void Update(Profile profile);
    bool Exists(Guid id);
    Task<Profile?> GetAsync(Guid id);
    Task<List<Profile>> GetAllAsync();
}
