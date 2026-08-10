namespace MafiCo.Console.Presentation.Base;

public record SwitchWindowRequest(
    Type WindowType
) : UiRequest;