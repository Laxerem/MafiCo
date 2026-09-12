using MafiCo.Application.Interfaces;
using MafiCo.Application.Llm.Commands;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.Exceptions;
using MediatR;

namespace MafiCo.Application.Llm.Handlers;

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
