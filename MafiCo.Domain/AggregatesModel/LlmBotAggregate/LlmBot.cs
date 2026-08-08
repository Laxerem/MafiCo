using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.LlmBotAggregate;

public class LlmBot : Entity, IAggregateRoot {
    public string ModelName { get; set; }
    public string Url {get; set;}
    public string ApiKey {get; set;}

    public LlmBot(string modelName, string url, string apiKey) {
        ModelName = modelName;
        Url = url;
        ApiKey = apiKey;
    }
}