using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Application.Game.Mediator.Internal.Events;

public record VotingFinishedEvent(PlayerId? Victim) : IGameEvent;