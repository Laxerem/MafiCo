namespace MafiCo.Domain.AggregatesModel.BotAggregate;

public interface IBotRepository {
    Task AddAsync(Bot botEntity);
    Task<Bot?> GetAsync(Guid id);
    Task<List<Bot>> GetAllAsync();
    Task<List<Bot>> GetAllAvailableAsync();
    Task<bool> ExistsByProfileIdAsync(Guid profileId);
    Task<bool> ExistsAsync(Guid id);
    Task RemoveAsync(Guid id);
}