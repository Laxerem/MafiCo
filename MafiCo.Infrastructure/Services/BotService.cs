using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastructure.DTOs;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence.Entities;

namespace MafiCo.Infrastructure.Services;

public class BotService {
    private readonly IBotRepository _repository;
    private readonly IProfileRepository _profileRepository;
    private readonly ILlmEntityRepository _llmRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BotService(
        IBotRepository repository,
        IProfileRepository profileRepository,
        ILlmEntityRepository llmRepository,
        IUnitOfWork unitOfWork) {
        _repository = repository;
        _profileRepository = profileRepository;
        _llmRepository = llmRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<BotDto>> GetAllBots() {
        var bots = await _repository.GetAllAsync();
        return bots
            .Select(bot => new BotDto(
                bot.Id,
                new ProfileInfo(bot.Profile.Id, bot.Profile.Name),
                bot.LlmEntity is not null ? LlmDto.FromEntity(bot.LlmEntity) : null))
            .ToList();
    }

    //Создание профиля здесь же
    public async Task CreateBot(string name, Guid llmId) {
        await EnsureLlmExistsAsync(llmId);

        var profile = _profileRepository.Add(Profile.Create(name));
        await _repository.AddAsync(new BotEntity(profile.Id, llmId));
        await _unitOfWork.SaveEntitiesAsync();
    }

    public async Task ChangeBotModel(Guid botId, Guid llmId) {
        var bot = await _repository.GetAsync(botId) ?? throw new BotException("Bot not found");
        await EnsureLlmExistsAsync(llmId);

        bot.ChangeLlm(llmId);
        await _unitOfWork.SaveEntitiesAsync();
    }

    public async Task DeleteBot(Guid botId) {
        if (!await _repository.ExistsAsync(botId)) {
            throw new BotException("Bot not found");
        }

        await _repository.RemoveAsync(botId);
        await _unitOfWork.SaveEntitiesAsync();
    }

    private async Task EnsureLlmExistsAsync(Guid llmId) {
        var llm = await _llmRepository.GetAsync(llmId);
        if (llm is null) {
            throw new BotException("Llm model not found");
        }
    }
}
