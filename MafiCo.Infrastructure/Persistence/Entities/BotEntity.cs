using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Infrastructure.Persistence.Entities;

public class BotEntity {
    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public Profile Profile { get; private set; } = null!;
    public Guid LlmId { get; private set; }
    public LlmEntity LlmEntity { get; private set; } = null!;

    public BotEntity(Guid profileId, Guid llmId) {
        Id = Guid.NewGuid();
        ProfileId = profileId;
        LlmId = llmId;
    }
}