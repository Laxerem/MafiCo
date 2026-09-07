namespace MafiCo.Console.Presentation.Features.Game.ControllerViews;

/// <summary>
/// Интерфейс управления игрой для игрока в текущей фазе.
/// Конкретная реализация выбирается по типу активного контроллера игрока
/// (см. <see cref="ControllerViewFactory"/>): чат, голосование мафии и т.д.
/// </summary>
internal interface IControllerView {
    /// <summary>
    /// Проводит один шаг взаимодействия с игроком (запрос ввода и отправка действия).
    /// </summary>
    Task RunTurnAsync();
}
