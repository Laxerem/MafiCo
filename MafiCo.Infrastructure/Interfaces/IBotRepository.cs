using MafiCo.Infrastructure.Persistence.Entities;

namespace MafiCo.Infrastructure.Interfaces;

public interface IBotRepository {
    Task AddAsync(BotEntity botEntity);
    Task<BotEntity?> GetAsync(Guid id);
    Task<List<BotEntity>> GetAllAsync();
    Task<List<BotEntity>> GetAllAvailableAsync();
    Task<bool> ExistsAsync(Guid id);
    Task RemoveAsync(Guid id);
}