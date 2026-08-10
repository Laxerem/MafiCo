using MafiCo.Console.Presentation.Base;

namespace MafiCo.Console.Presentation.Windows.Lobby.Requests;

public record ChangeNameRequest(string Name) : UiRequest;