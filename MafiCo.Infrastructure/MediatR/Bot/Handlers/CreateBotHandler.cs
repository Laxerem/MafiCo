using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.MediatR.Bot.Commands;
using MediatR;
using DomainBot = MafiCo.Domain.AggregatesModel.BotAggregate.Bot;
using DomainProfile = MafiCo.Domain.AggregatesModel.ProfileAggregate.Profile;

namespace MafiCo.Infrastructure.MediatR.Bot.Handlers;

public class CreateBotHandler : IRequestHandler<CreateBotCommand> {
    private readonly IBotRepository _botRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly ILlmRepository _llmRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBotHandler(
        IBotRepository botRepository,
        IProfileRepository profileRepository,
        ILlmRepository llmRepository,
        IUnitOfWork unitOfWork) {
        _botRepository = botRepository;
        _profileRepository = profileRepository;
        _llmRepository = llmRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateBotCommand request, CancellationToken cancellationToken) {
        if (await _llmRepository.GetAsync(request.LlmId) is null) {
            throw new BotException("Llm model not found");
        }

        var profile = _profileRepository.Add(DomainProfile.Create(request.Name));
        await _botRepository.AddAsync(new DomainBot(profile.Id, request.LlmId));
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
