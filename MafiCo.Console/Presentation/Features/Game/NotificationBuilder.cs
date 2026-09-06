using MafiCo.Application.Notifications;
using MediatR;

namespace MafiCo.Console.Presentation.Features.Game;

public static class NotificationBuilder {
    public static string Build(INotification notification) {
        switch (notification) {
            case RoleAssignedNotification notify:
                return $"Роль: {notify.Role}";
        }
        throw new NotImplementedException($"Builder for notification {notification.GetType()} doesn't exists");
    }
}