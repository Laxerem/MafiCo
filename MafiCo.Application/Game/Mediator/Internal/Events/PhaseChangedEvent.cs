using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;

namespace MafiCo.Application.Game.Mediator.Internal.Events;

internal record PhaseChangedEvent(GamePhase Phase) : IGameEvent;