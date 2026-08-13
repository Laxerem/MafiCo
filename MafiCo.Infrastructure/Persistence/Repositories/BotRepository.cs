using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<BotEntity>> GetAllAsync() {
        return await _context.Bots
            .AsNoTracking()
            .Include(x => x.Profile)
            .Include(x => x.LlmEntity)
            .ToListAsync();
    }

    public async Task<List<BotEntity>> GetAllAvailableAsync() {
        return await _context.Bots
            .AsNoTracking()
            .Where(x => x.LlmId != null)
            .Include(x => x.Profile)
            .Include(x => x.LlmEntity)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid id) {
        return await _context.Bots.AnyAsync(x => x.Id == id);
    }

    public async Task RemoveAsync(Guid id) {
        var bot = await _context.Bots.FindAsync(id);
        if (bot is not null) {
            _context.Bots.Remove(bot);
        }
    }
}
