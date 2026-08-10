using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.LlmBotAggregate;

public class LlmBot : Entity, IAggregateRoot {
    public string ModelName { get; private set; }
    public string Url {get; private set;}
    public string ApiKey {get; private set;}

    public LlmBot(string modelName, string url, string apiKey) : base(Guid.NewGuid()) {
        ModelName = modelName;
        Url = url;
        ApiKey = apiKey;
    }

    public void UpdateModel(string modelName, string url, string apiKey) {
        ModelName = modelName;
        Url = url;
        ApiKey = apiKey;
    }
}