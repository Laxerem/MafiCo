using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Windows.Lobby.Events;

namespace MafiCo.Console.Presentation.Windows.Lobby;

public class LlmBotCreatingWindow : Window {
    public async override Task Show() {
        var modelName = await AppInterface.GetUserInput("Model name:");
        var providerUrl = await AppInterface.GetUserInput("Provider url:");
        var apiKey = await AppInterface.GetUserInput("Api key:");

        await RaiseEvent(new LlmBotCreatedEvent(modelName, providerUrl, apiKey));
    }
}