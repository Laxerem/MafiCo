using MafiCo.Domain.DTOs;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events.Profile;

public record ProfileCreatedEvent(AggregatesModel.ProfileAggregate.Profile Profile) : IDomainEvent;