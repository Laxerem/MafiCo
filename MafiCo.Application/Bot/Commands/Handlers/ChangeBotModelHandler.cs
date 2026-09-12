using MafiCo.Application.Bot.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.Exceptions;
using MediatR;

namespace MafiCo.Application.Bot.Commands.Handlers;

public class ChangeBotModelHandler : IRequestHandler<ChangeBotModelCommand> {
    private readonly IBotRepository _botRepository;
    private readonly ILlmRepository _llmRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeBotModelHandler(
        IBotRepository botRepository,
        ILlmRepository llmRepository,
        IUnitOfWork unitOfWork) {
        _botRepository = botRepository;
        _llmRepository = llmRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeBotModelCommand request, CancellationToken cancellationToken) {
        var bot = await _botRepository.GetAsync(request.BotId)
            ?? throw new BotException("Bot not found");

        if (await _llmRepository.GetAsync(request.LlmId) is null) {
            throw new BotException("Llm model not found");
        }

        bot.ChangeLlm(request.LlmId);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
