using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;

namespace MafiCo.Application.Game.Notifications;

public record PhaseChangedNotification(GamePhase Phase) : IGameNotification {}