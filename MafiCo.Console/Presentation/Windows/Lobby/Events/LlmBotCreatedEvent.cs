using MafiCo.Console.Presentation.Events.Common;

namespace MafiCo.Console.Presentation.Windows.Lobby.Events;

public record LlmBotCreatedEvent(string ModelName, string Url, string ApiKey) : UiEvent;