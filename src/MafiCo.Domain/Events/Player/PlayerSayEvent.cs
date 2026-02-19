using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events.Player;

public record PlayerSayEvent(string Username, string Text) : INotification, IDomainEvent;