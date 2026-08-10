using MafiCo.Console.Presentation.Base;

namespace MafiCo.Console.Presentation.Windows.Lobby.Events;

public record CreateLlmBotRequest(string ModelName, string Url, string ApiKey) : UiRequest;