using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.MediatR.Llm.Commands;
using MediatR;
using DomainLlm = MafiCo.Domain.AggregatesModel.LlmAggregate.Llm;

namespace MafiCo.Infrastructure.MediatR.Llm.Handlers;

public class CreateLlmHandler : IRequestHandler<CreateLlmCommand> {
    private readonly ILlmRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLlmHandler(ILlmRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateLlmCommand request, CancellationToken cancellationToken) {
        _repository.Add(new DomainLlm(request.ModelName, request.Url, request.ApiKey));
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
