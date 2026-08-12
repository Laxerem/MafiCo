using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence.Entities;
using MediatR;

namespace MafiCo.Console.Handlers;

public class LlmCreatingEventHandler : INotificationHandler<CreateLlm> {
    private readonly ILlmEntityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public LlmCreatingEventHandler(ILlmEntityRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateLlm evt, CancellationToken cancellationToken) {
        var model = new LlmEntity(evt.ModelName, evt.Url, evt.ApiKey);
        _repository.Add(model);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
