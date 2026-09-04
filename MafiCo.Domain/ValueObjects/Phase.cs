using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.ValueObjects;

public abstract class Phase {
    public readonly PhaseType Type;
    public readonly int DurationSeconds;

    public Phase(PhaseType type, int durationSeconds) {
        Type = type;
        DurationSeconds = durationSeconds;
    }
}