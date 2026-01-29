using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events;

public record MafiaSelectTargetEvent(string targetName) : INotification;