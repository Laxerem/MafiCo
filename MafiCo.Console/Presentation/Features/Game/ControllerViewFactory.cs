using MafiCo.Application.Interfaces.Controllers;
using MafiCo.Console.Presentation.Features.Game.ControllerViews;
using MafiCo.Infrastructure.Controllers;
using MafiCo.Infrastructure.DTOs;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// По активному контроллеру игрока подбирает способ управления игрой.
/// Пока контроллер не назначен (игрок не ходит в этой фазе), возвращает <c>null</c>.
/// </summary>
internal static class ControllerViewFactory {
    public static IControllerView? Create(
        IPlayerController? controller,
        IReadOnlyList<PublicPlayerInfo> players,
        Guid selfId) => controller switch {
        DefaultController defaultController => new ChatControllerView(defaultController),
        VoterController voterController => new VoteControllerView(voterController, players, selfId),
        _ => null,
    };
}
