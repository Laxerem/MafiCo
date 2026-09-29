using MafiCo.Domain.AggregatesModel.LlmAggregate;

namespace MafiCo.Application.Llm;

public record LlmDto(
    LlmId Id,
    string ModelName,
    string Url
) {
    public static LlmDto FromEntity(Domain.AggregatesModel.LlmAggregate.Llm llm) =>
        new(llm.Id, llm.ModelName, llm.Url);
}
