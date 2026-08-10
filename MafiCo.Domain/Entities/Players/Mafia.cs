namespace MafiCo.Domain.Entities.Players;

public class Mafia(Guid id) : Player(id) {
    protected override Role Role { get; init; } = Role.Mafia;
}