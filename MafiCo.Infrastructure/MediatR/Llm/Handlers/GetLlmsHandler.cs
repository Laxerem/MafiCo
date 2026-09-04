using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Infrastructure.MediatR.Llm.Commands;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Llm.Handlers;

public class GetLlmsHandler : IRequestHandler<GetLlmsCommand, List<LlmDto>> {
    private readonly ILlmRepository _repository;

    public GetLlmsHandler(ILlmRepository repository) {
        _repository = repository;
    }

    public async Task<List<LlmDto>> Handle(GetLlmsCommand request, CancellationToken cancellationToken) {
        var models = await _repository.GetAllAsync();
        return models.Select(LlmDto.FromEntity).ToList();
    }
}
