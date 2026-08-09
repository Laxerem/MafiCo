namespace MafiCo.Console.Presentation.Base;

public record SwitchWindowEvent(
    Type WindowType
) : UiEvent;