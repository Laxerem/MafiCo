using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public class Player : Entity {
    public Role? Role { get; private set; }

    public Player(Guid id) : base(id) {
    }

    public void AssignRole(Role role) {
        if (Role is not null) {
            throw new DomainException("Player role is already assigned");
        }

        Role = role;
    }
}
