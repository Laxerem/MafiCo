namespace MafiCo.Domain.AggregatesModel.LlmAggregate;

public interface ILlmRepository {
    Llm Add(Llm llmEntity);
    void Update(Llm llmEntity);
    Task<Llm?> GetAsync(LlmId id);
    Task<List<Llm>> GetAllAsync();
    Task<bool> ExistsAsync(LlmId id);
    Task RemoveAsync(LlmId id);
}