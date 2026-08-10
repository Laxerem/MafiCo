using MafiCo.Domain.Events.Players;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Entities.Players;

public abstract class Player(Guid id) : Entity(id) {

    protected abstract Role Role { get; init; }

    public void Vote(Guid targetId) {
        if (targetId == Id) {
            throw new PlayerException("You cannot vote for yourself.");
        }
    }
    public Role GetRole() => Role;
}