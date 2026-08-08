using MafiCo.Console.Presentation.Events.Common;

namespace MafiCo.Console.Presentation.Events;

public record SwitchWindowEvent(
    Type WindowType
) : UiEvent;