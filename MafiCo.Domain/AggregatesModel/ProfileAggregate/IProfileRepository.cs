namespace MafiCo.Domain.AggregatesModel.ProfileAggregate;

public interface IProfileRepository {
    Profile Add(Profile profile);
    void Update(Profile profile);
    Task<Profile?> GetAsync(Guid id);
    Task<List<Profile>> GetAllAsync();
}