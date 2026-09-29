using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Events;

public record PlayerKilledDomainEvent(PlayerId Id, Role Role, Audience Audience) : IGameDomainEvent {}
