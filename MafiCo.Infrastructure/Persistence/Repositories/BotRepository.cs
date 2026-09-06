using MafiCo.Domain.AggregatesModel.BotAggregate;
using Microsoft.EntityFrameworkCore;

namespace MafiCo.Infrastructure.Persistence.Repositories;

public class BotRepository : IBotRepository {
    private readonly ApplicationContext _context;

    public BotRepository(ApplicationContext context) {
        _context = context;
    }

    public Task AddAsync(Bot botEntity) {
        _context.Bots.Add(botEntity);
        return Task.CompletedTask;
    }

    public async Task<Bot?> GetAsync(Guid id) {
        return await _context.Bots.FindAsync(id);
    }

    public async Task<List<Bot>> GetAllAsync() {
        return await _context.Bots
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Bot>> GetAllAvailableAsync() {
        return await _context.Bots
            .AsNoTracking()
            .Where(x => x.LlmId != null)
            .ToListAsync();
    }

    public async Task<bool> ExistsByProfileIdAsync(Guid profileId) {
        return await _context.Bots.AnyAsync(x => x.ProfileId == profileId);
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
