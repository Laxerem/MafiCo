using MafiCo.Console.Presentation.Windows.Lobby.Events;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.SeedWork;
using MafiCo.Infrastructure.Interfaces;
using MediatR;

namespace MafiCo.Console.Presentation.Windows.Lobby.Handlers;

public class LlmBotCreatedEventHandler : INotificationHandler<CreateLlmBot> {
    private readonly ILlmBotRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public LlmBotCreatedEventHandler(ILlmBotRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(CreateLlmBot evt, CancellationToken cancellationToken) {
        var model = new LlmBot(evt.ModelName, evt.Url, evt.ApiKey);
        _repository.Add(model);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}