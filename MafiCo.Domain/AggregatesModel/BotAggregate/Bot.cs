using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.BotAggregate;

public class Bot : Entity, IAggregateRoot {
    public Guid ProfileId { get; private set; }
    public Guid? LlmId { get; private set; }

    private Bot() : base(Guid.NewGuid()) {
    }

    public Bot(Guid profileId, Guid? llmId) : base(Guid.NewGuid()) {
        ProfileId = profileId;
        LlmId = llmId;
    }

    public void ChangeLlm(Guid llmId) {
        LlmId = llmId;
    }
}
