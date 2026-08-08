namespace MafiCo.Domain.Events.Profile;

public record ProfileCreatedEvent(AggregatesModel.ProfileAggregate.Profile Profile);