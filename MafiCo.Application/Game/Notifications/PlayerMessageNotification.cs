using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Game.Notifications;

public record PlayerMessageNotification(string Name, string Message) : IGameNotification;