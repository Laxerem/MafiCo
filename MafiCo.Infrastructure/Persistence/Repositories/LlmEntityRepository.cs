using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace MafiCo.Infrastructure.Persistence.Repositories;

public class LlmEntityRepository : ILlmEntityRepository {
    private readonly ApplicationContext _context;
    
    public LlmEntityRepository(ApplicationContext context) {
        _context = context;
    }
    
    public LlmEntity Add(LlmEntity llmEntity) {
        return _context.LlmBots.Add(llmEntity).Entity;
    }

    public void Update(LlmEntity llmEntity) {
        throw new NotImplementedException();
    }

    public async Task<LlmEntity?> GetAsync(Guid id) {
        return await _context.LlmBots.FindAsync(id);
    }

    public async Task<List<LlmEntity>> GetAllAsync() {
        return await _context.LlmBots
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid id) {
        return await _context.LlmBots.AnyAsync(x => x.Id == id);
    }

    public async Task RemoveAsync(Guid id) {
        var model = await _context.LlmBots.FindAsync(id);
        if (model is not null) {
            _context.LlmBots.Remove(model);
        }
    }
}