using MafiCo.Domain.AggregatesModel.BotAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.BotAggregate;

public class Bot : AggregateRoot<BotId> {
    public ProfileId ProfileId { get; private set; }
    public LlmId? LlmId { get; private set; }

    private Bot() : base(new BotId(Guid.NewGuid())) {}

    public Bot(ProfileId profileId, LlmId? llmId) : base(new BotId(Guid.NewGuid())) {
        ProfileId = profileId;
        LlmId = llmId;
    }

    public void ChangeLlm(LlmId llmId) {
        LlmId = llmId;
    }
}
