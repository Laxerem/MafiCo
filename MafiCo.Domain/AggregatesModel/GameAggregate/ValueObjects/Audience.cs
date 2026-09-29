using MafiCo.Domain.AggregatesModel.GameAggregate.Entities;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

public sealed record Audience(IEnumerable<PlayerId> Players);