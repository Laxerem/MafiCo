using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game.Notifications;

public record GameFinishedNotification(IReadOnlyList<ResultPlayerInfo> Winners, IReadOnlyList<ResultPlayerInfo> Losers) : IGameNotification;