using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Entities;

public class Player : Entity {
    public bool IsAlive { get; private set; }
    public Role? Role { get; private set; }

    public Player(Guid id) : base(id) {
        IsAlive = true;
    }

    public void AssignRole(Role role) {
        if (Role is not null) {
            throw new DomainException("Player role is already assigned");
        }

        Role = role;
    }
    
    public void Kill() {
        if (!IsAlive) throw new DomainException("You cannot kill a dead player");
        IsAlive = false;
    }
}
