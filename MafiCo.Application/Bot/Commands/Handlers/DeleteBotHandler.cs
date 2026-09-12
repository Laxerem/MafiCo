using MafiCo.Application.Bot.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.Exceptions;
using MediatR;

namespace MafiCo.Application.Bot.Commands.Handlers;

public class DeleteBotHandler : IRequestHandler<DeleteBotCommand> {
    private readonly IBotRepository _botRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBotHandler(IBotRepository botRepository, IUnitOfWork unitOfWork) {
        _botRepository = botRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteBotCommand request, CancellationToken cancellationToken) {
        if (!await _botRepository.ExistsAsync(request.BotId)) {
            throw new BotException("Bot not found");
        }

        await _botRepository.RemoveAsync(request.BotId);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
