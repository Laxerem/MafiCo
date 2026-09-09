using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;

namespace MafiCo.Application.Notifications.GameNotifications;

public record PhaseChangedNotification(GamePhase Phase) : IGameNotification {}