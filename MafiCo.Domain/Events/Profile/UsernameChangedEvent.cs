using MafiCo.Domain.Interfaces;
using MediatR;

namespace MafiCo.Domain.Events.Profile;

public record UsernameChangedEvent(Guid ProfileId, string NewUsername) : IDomainEvent;