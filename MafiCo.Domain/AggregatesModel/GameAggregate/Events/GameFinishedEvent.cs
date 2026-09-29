using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Events;

public record GameFinishedEvent(IReadOnlyList<PlayerInfo> Winners, IReadOnlyList<PlayerInfo> Losers, Audience Audience) : IGameDomainEvent {}
