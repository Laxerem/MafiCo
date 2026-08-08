using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events.Profile;

public record UsernameChangedEvent(Guid ProfileId, string NewUsername) : IDomainEvent;