using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Events;

public record RoleIsDetermineEvent(string username, Role Role) : IGameEvent;