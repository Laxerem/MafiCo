using MafiCo.Application.Game;
using MafiCo.Application.Game.Controllers;
using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Interfaces;
using MafiCo.Console.Presentation.Features.Game.ControllerViews;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// По активному контроллеру игрока подбирает способ управления игрой.
/// Пока контроллер не назначен (игрок не ходит в этой фазе), возвращает <c>null</c>.
/// </summary>
internal static class ControllerViewFactory {
    public static IControllerView? Create(
        IPlayerController? controller,
        IReadOnlyList<PublicPlayerInfo> players,
        ProfileId selfId) => controller switch {
        DefaultController defaultController => new ChatControllerView(defaultController),
        VoterController voterController => new VoteControllerView(voterController, players, new PlayerId(selfId.Id)),
        _ => null,
    };
}
