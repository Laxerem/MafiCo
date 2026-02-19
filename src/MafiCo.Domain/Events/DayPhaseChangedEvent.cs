using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Events;

public record DayPhaseChangedEvent(DayPhase NewPhase) : IDomainEvent;