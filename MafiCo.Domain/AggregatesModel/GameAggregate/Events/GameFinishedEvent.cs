using MafiCo.Domain.DTOs;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Events;

public record GameFinishedEvent(IReadOnlyList<PlayerInfo> Winners, IReadOnlyList<PlayerInfo> Losers) : IDomainEvent;
