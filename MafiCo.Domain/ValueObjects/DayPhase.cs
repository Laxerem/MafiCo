using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.ValueObjects;

public class DayPhase : Phase {
    public DayPhase(int durationSeconds) : base(PhaseType.Day, durationSeconds) {
        
    }
}