using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public interface IGameDomainEvent : IDomainEvent {
    Audience Audience { get; }
}