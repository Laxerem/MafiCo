using MafiCo.Domain.Entities;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events;

public record PlayerVotingEvent(string Username) : INotification, IGameEvent;