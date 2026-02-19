using System.Text;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Infrastracture.Brains;

public class BotBrain : IPlayerBrain {
    private IOpenRouterClient _client;
    private string _systemPrompt;
    private string _modelName;
    
    private StringBuilder _contextBuilder = new();
    
    public BotBrain(IOpenRouterClient client, string systemPrompt, string modelName) {
        _client = client;
        _systemPrompt = systemPrompt;
        _modelName = modelName;
        _contextBuilder.AppendLine(_systemPrompt);
    }
    
    public async Task<string> ChooseTargetAsync() {
        throw new NotImplementedException();
    }
}