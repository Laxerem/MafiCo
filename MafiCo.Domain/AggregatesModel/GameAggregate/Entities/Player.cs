using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Entities;

public sealed class Player : Entity<PlayerId> {
    public bool IsAlive { get; private set; }
    public Role? Role { get; private set; }

    public Player(PlayerId id) : base(id) {
        IsAlive = true;
    }

    internal void AssignRole(Role role) {
        if (Role is not null) {
            throw new DomainException("Player role is already assigned");
        }

        Role = role;
    }
    
    internal void Die() {
        if (!IsAlive) throw new DomainException("You cannot kill a dead player");
        IsAlive = false;
    }
}
