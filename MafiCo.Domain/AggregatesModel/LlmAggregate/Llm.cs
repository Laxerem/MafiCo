using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.LlmAggregate;

public class Llm : Entity, IAggregateRoot {
    public string ModelName { get; private set; }
    public string Url { get; private set; }
    public string ApiKey { get; private set; }

    private Llm() : base(Guid.NewGuid()) {
    }

    public Llm(string modelName, string url, string apiKey) : base(Guid.NewGuid()) {
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
