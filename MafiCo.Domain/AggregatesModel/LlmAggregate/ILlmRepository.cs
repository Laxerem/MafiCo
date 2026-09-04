namespace MafiCo.Domain.AggregatesModel.LlmAggregate;

public interface ILlmRepository {
    Llm Add(Llm llmEntity);
    void Update(Llm llmEntity);
    Task<Llm?> GetAsync(Guid id);
    Task<List<Llm>> GetAllAsync();
    Task<bool> ExistsAsync(Guid id);
    Task RemoveAsync(Guid id);
}