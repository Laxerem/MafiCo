using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Infrastructure.Persistence.Entities;

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
}