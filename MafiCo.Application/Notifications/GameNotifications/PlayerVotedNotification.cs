using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Notifications.GameNotifications;

public record PlayerVotedNotification(string PlayerName, string TargetName) : IGameNotification;