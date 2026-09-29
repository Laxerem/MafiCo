using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Application.Game.Notifications.Players;

public record PlayerExcludedNotification(PlayerId? PlayerId, string? PlayerName) : IGameNotification;