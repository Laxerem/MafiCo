using MafiCo.Infrastructure.Persistence.Entities;

namespace MafiCo.Domain.AggregatesModel.LlmBotAggregate;

public interface ILlmEntityRepository {
    LlmEntity Add(LlmEntity llmEntity);
    void Update(LlmEntity llmEntity);
    Task<LlmEntity?> GetAsync(Guid id);
}