namespace MafiCo.Domain.AggregatesModel.LlmBotAggregate;

public interface ILlmBotRepository {
    LlmBot Add(LlmBot llmBot);
    void Update(LlmBot llmBot);
    Task<LlmBot?> GetAsync(Guid id);
}