using MafiCo.Console.Presentation.Base;

namespace MafiCo.Console.Presentation.Windows.Lobby.Events;

public record CreateLlmBot(string ModelName, string Url, string ApiKey) : UseCase;