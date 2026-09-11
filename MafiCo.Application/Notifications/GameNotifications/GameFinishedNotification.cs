using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Notifications.GameNotifications;

public record GameFinishedNotification(IReadOnlyList<PlayerInfo> Winners, IReadOnlyList<PlayerInfo> Losers) : IGameNotification;