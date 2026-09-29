using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game.Notifications;

public record PlayerKilledNotification(string PlayerName) : IGameNotification;