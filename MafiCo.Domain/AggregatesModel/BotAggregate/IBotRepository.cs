using MafiCo.Domain.AggregatesModel.BotAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Domain.AggregatesModel.BotAggregate;

public interface IBotRepository {
    Task AddAsync(Bot botEntity);
    Task<Bot?> GetAsync(BotId id);
    Task<List<Bot>> GetAllAsync();
    Task<List<Bot>> GetAllAvailableAsync();
    Task<bool> ExistsByProfileIdAsync(ProfileId profileId);
    Task<bool> ExistsAsync(BotId id);
    Task RemoveAsync(BotId id);
}
