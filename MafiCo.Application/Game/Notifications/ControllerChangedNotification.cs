using MafiCo.Application.Game;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game.Notifications;

public record ControllerChangedNotification(IPlayerController? Controller) : IGameNotification {}