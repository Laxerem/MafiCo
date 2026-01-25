using MafiCo.Domain.Abstractions;

namespace MafiCo.Domain.Entities;

public class BotProfile : BaseProfile {
    public string ModelName { get; private set; }
    public string SystemPrompt { get; private set; }
    
    public BotProfile(int id, string username, string modelName, string systemPrompt) : base(id, username) {
        ModelName = modelName;
        SystemPrompt = systemPrompt;
    }
}