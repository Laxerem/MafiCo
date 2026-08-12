using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence.Entities;

namespace MafiCo.Infrastructure.Services;

public class LlmService {
    private readonly ILlmEntityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public LlmService(ILlmEntityRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(string modelName, string url, string apiKey) {
        var model = new LlmEntity(modelName, url, apiKey);
        _repository.Add(model);
        await _unitOfWork.SaveEntitiesAsync();
    }
}
