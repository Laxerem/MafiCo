using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence.Entities;

namespace MafiCo.Infrastructure.Persistence.Repositories;

public class BotRepository : IBotRepository {
    private readonly ApplicationContext _context;

    public BotRepository(ApplicationContext context) {
        _context = context;
    }

    public Task AddAsync(BotEntity botEntity) {
        _context.Bots.Add(botEntity);
        return Task.CompletedTask;
    }

    public async Task<BotEntity?> GetAsync(Guid id) {
        return await _context.Bots.FindAsync(id);
    }
}
