using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Notifications.GameNotifications;

public record PlayerMessageNotification(string Name, string Message) : IGameNotification;