using MafiCo.Application.Interfaces;

namespace MafiCo.Infrastracture.Brains;

public class BotBrain : IPlayerBrain {
    private IOpenRouterClient _client;
    private string _systemPrompt;
    private string _modelName;
    
    public BotBrain(IOpenRouterClient _client, string systemPrompt, string modelName) {
        _systemPrompt = systemPrompt;
        _modelName = modelName;
    }
    
    public async Task<string> ChooseTargetAsync() {
        throw new NotImplementedException();
    }
}