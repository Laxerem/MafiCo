using MafiCo.Domain.DTOs;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Events;

public record GameFinishedEvent(IReadOnlyList<PlayerInfo> Winners, IReadOnlyList<PlayerInfo> Losers) : IGameDomainEvent;
