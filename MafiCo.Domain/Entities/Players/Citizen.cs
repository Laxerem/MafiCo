namespace MafiCo.Domain.Entities.Players;

public class Citizen(Guid id) : Player(id) {
    protected override Role Role { get; init; } = Role.Citizen;
}