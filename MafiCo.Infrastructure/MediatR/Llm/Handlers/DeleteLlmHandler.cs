using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.MediatR.Llm.Commands;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Llm.Handlers;

public class DeleteLlmHandler : IRequestHandler<DeleteLlmCommand> {
    private readonly ILlmRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLlmHandler(ILlmRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteLlmCommand request, CancellationToken cancellationToken) {
        if (!await _repository.ExistsAsync(request.LlmId)) {
            throw new LlmException("Model not found");
        }

        await _repository.RemoveAsync(request.LlmId);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
