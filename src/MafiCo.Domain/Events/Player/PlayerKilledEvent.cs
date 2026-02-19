using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Events;

public record PlayerKilledEvent(string Username) : INotification,  IDomainEvent;