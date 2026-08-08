using MafiCo.Domain.AggregatesModel.LlmBotAggregate;

namespace MafiCo.Infrastructure.Repositories;

public class LlmBotRepository : ILlmBotRepository {
    private readonly ApplicationContext _context;
    
    public LlmBotRepository(ApplicationContext context) {
        _context = context;
    }
    
    public LlmBot Add(LlmBot llmBot) {
        return _context.LlmBots.Add(llmBot).Entity;
    }

    public void Update(LlmBot llmBot) {
        throw new NotImplementedException();
    }

    public async Task<LlmBot?> GetAsync(Guid id) {
        return await _context.LlmBots.FindAsync(id);
    }
}