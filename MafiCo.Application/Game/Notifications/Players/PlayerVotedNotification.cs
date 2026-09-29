using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game.Notifications;

public record PlayerVotedNotification(string PlayerName, string TargetName) : IGameNotification;