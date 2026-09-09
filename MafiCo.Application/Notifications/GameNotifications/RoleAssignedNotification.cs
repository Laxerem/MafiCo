using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Notifications.GameNotifications;

public record RoleAssignedNotification(Role Role) : IPlayerNotification;