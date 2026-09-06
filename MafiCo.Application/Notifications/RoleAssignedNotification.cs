using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Notifications;

public record RoleAssignedNotification(Role Role) : IPlayerNotification;