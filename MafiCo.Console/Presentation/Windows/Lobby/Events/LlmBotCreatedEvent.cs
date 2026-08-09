using MafiCo.Console.Presentation.Base;

namespace MafiCo.Console.Presentation.Windows.Lobby.Events;

public record LlmBotCreatedEvent(string ModelName, string Url, string ApiKey) : UiEvent;