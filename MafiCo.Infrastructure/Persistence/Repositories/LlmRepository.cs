using MafiCo.Domain.AggregatesModel.LlmAggregate;
using Microsoft.EntityFrameworkCore;

namespace MafiCo.Infrastructure.Persistence.Repositories;

public class LlmRepository : ILlmRepository {
    private readonly ApplicationContext _context;
    
    public LlmRepository(ApplicationContext context) {
        _context = context;
    }
    
    public Llm Add(Llm llmEntity) {
        return _context.LlmBots.Add(llmEntity).Entity;
    }

    public void Update(Llm llmEntity) {
        throw new NotImplementedException();
    }

    public async Task<Llm?> GetAsync(Guid id) {
        return await _context.LlmBots.FindAsync(id);
    }

    public async Task<List<Llm>> GetAllAsync() {
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