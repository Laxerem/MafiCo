using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Application.Game.Mediator.Internal.Events;

internal record PhaseChangedEvent(GamePhase Phase, IEnumerable<PlayerId> ActivePlayers) : IGameEvent;