namespace MafiCo.Infrastructure.MediatR.Llm;

public record LlmDto(
    Guid Id,
    string ModelName,
    string Url
) {
    public static LlmDto FromEntity(Domain.AggregatesModel.LlmAggregate.Llm llm) =>
        new(llm.Id, llm.ModelName, llm.Url);
}
