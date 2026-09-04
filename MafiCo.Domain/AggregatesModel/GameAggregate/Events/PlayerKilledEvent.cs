using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Events;

public record PlayerKilledEvent(Guid PlayerId, Role Role) : IDomainEvent;
