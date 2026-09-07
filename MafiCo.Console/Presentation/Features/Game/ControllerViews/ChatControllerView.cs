using MafiCo.Infrastructure.Controllers;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game.ControllerViews;

/// <summary>
/// Управление игрой через чат: игрок вводит строку, она уходит в игру как сообщение.
/// Соответствует <see cref="DefaultController"/>.
/// </summary>
internal sealed class ChatControllerView : IControllerView {
    private readonly DefaultController _controller;

    public ChatControllerView(DefaultController controller) {
        _controller = controller;
    }

    public async Task RunTurnAsync() {
        var message = await AnsiConsole.PromptAsync(
            new TextPrompt<string>("[green]Вы[/]:").AllowEmpty());

        if (!string.IsNullOrWhiteSpace(message)) {
            await _controller.SendMessage(message);
        }
    }
}
