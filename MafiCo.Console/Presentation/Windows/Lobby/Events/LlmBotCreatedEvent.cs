using MafiCo.Console.Presentation.Events.Common;

namespace MafiCo.Console.LobbyContext.Events;

public record LlmBotCreatedEvent(string ModelName, string Url, string ApiKey) : UiEvent;