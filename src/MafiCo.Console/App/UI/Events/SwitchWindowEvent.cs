using MafiCo.Console.App.UI.Common;

namespace MafiCo.Console.App.UI.Events;

public record SwitchWindowEvent(
    Type WindowType
) : UiEvent;