using MafiCo.Console.LobbyContext.Events;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.SeedWork;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.LobbyContext.Handlers;

public class LlmBotCreatedEventHandler : INotificationHandler<LlmBotCreatedEvent> {
    private readonly ILlmBotRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public LlmBotCreatedEventHandler(ILlmBotRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(LlmBotCreatedEvent evt, CancellationToken cancellationToken) {
        var model = new LlmBot(evt.ModelName, evt.Url, evt.ApiKey);
        _repository.Add(model);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}