using MafiCo.Application.Game.Notifications;
using MediatR;

namespace MafiCo.Console.Presentation.Features.Game;

public static class NotificationBuilder {
    public static string Build(INotification notification) {
        switch (notification) {
            case RoleAssignedNotification notify:
                return $"Роль: {notify.Role}";
            case PhaseChangedNotification notify:
                return $"Фаза: {notify.Phase}";
            case PlayerVotedNotification notify:
                return $"{notify.PlayerName} голосует против {notify.TargetName}";
            case PlayerKilledNotification notify:
                return $"{notify.PlayerName} был убит";
        }
        throw new NotImplementedException($"Builder for notification {notification.GetType()} doesn't exists");
    }
}