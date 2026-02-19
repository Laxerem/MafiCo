using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Events;

public record RoleDeterminateEvent (
    string PlayerName,
    Role Role
) : IDomainEvent;