using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Infrastracture.Events;

public record UserRoleIsDetermine(Role Role) : IDomainEvent;