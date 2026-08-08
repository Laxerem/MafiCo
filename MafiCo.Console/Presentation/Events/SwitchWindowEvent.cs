using MafiCo.Console.App.UI.Events;

namespace MafiCo.Console.Presentation.Events;

public record SwitchWindowEvent(
    Type WindowType
) : UiEvent;