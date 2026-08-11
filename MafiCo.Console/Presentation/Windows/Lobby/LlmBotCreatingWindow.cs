using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.Events;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class LlmBotCreatingWindow : Window {
    public async override Task Show() {
        var modelName = await AppComponents.GetUserInput("Model name:");
        var providerUrl = await AppComponents.GetUserInput("Provider url:");
        var apiKey = await AppComponents.GetUserInput("Api key:");
        
        await UseAsync(new CreateLlmBot(modelName, providerUrl, apiKey));
    }
}