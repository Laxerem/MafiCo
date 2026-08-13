using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastructure.DTOs;
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

    public async Task<List<LlmDto>> GetAllAsync() {
        var models = await _repository.GetAllAsync();
        return models.Select(LlmDto.FromEntity).ToList();
    }

    public async Task DeleteAsync(Guid id) {
        if (!await _repository.ExistsAsync(id)) {
            throw new LlmException("Model not found");
        }

        await _repository.RemoveAsync(id);
        await _unitOfWork.SaveEntitiesAsync();
    }
}
