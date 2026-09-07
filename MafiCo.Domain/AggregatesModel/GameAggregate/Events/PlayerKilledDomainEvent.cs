using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Events;

public record PlayerKilledDomainEvent(Guid PlayerId, Role Role) : IGameDomainEvent;
