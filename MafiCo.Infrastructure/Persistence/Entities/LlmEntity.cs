namespace MafiCo.Infrastructure.Persistence.Entities;

public class LlmEntity {
    public Guid Id { get; init; }
    public string ModelName { get; private set; }
    public string Url {get; private set;}
    public string ApiKey {get; private set;}

    public LlmEntity(string modelName, string url, string apiKey) {
        Id = Guid.NewGuid();
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