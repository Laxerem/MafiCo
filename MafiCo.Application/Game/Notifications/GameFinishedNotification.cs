using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Game.Notifications;

public record GameFinishedNotification(IReadOnlyList<PlayerInfo> Winners, IReadOnlyList<PlayerInfo> Losers) : IGameNotification;