using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Game.Notifications;

public record RoleAssignedNotification(Role Role) : IPlayerNotification;