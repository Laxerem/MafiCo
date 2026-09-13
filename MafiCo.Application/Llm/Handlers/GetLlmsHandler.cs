using MafiCo.Application.Llm.Commands;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MediatR;

namespace MafiCo.Application.Llm.Handlers;

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
