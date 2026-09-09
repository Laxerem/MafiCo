using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;
using MafiCo.Infrastructure.MediatR.Bot.Commands;
using MafiCo.Infrastructure.MediatR.Llm;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Bot.Handlers;

public class GetBotsHandler : IRequestHandler<GetBotsCommand, List<BotDto>> {
    private readonly IBotRepository _botRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly ILlmRepository _llmRepository;

    public GetBotsHandler(
        IBotRepository botRepository,
        IProfileRepository profileRepository,
        ILlmRepository llmRepository) {
        _botRepository = botRepository;
        _profileRepository = profileRepository;
        _llmRepository = llmRepository;
    }

    public async Task<List<BotDto>> Handle(GetBotsCommand request, CancellationToken cancellationToken) {
        var bots = await _botRepository.GetAllAsync();
        if (bots.Count == 0) {
            return [];
        }

        var profiles = (await _profileRepository.GetAllAsync()).ToDictionary(profile => profile.Id);
        var models = (await _llmRepository.GetAllAsync()).ToDictionary(model => model.Id);

        return bots
            .Select(bot => {
                var profile = profiles[bot.ProfileId];
                var model = bot.LlmId is { } llmId && models.TryGetValue(llmId, out var found)
                    ? LlmDto.FromEntity(found)
                    : null;

                return new BotDto(bot.Id, new ProfileInfo(profile.Id, profile.Name), model);
            })
            .ToList();
    }
}
