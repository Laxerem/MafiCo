using MafiCo.Infrastructure.Persistence.Entities;

namespace MafiCo.Infrastructure.DTOs;

public record LlmDto (
    Guid Id,
    string ModelName,
    string Url
) {
    public static LlmDto FromEntity(LlmEntity entity) => new(entity.Id, entity.ModelName, entity.Url);
}
