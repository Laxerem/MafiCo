using MafiCo.Domain.Events.Players;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Entities.Players;

public abstract class Player : Entity {
    public readonly string Name;
    private Role _role;

    public Player(string name, Role role) {
        Name = name;
        _role = role;
    }

    public void Vote(Player target) {
        if (target.Id == Id) {
            throw new PlayerException("You cannot vote for yourself.");
        }
        AddNotification(new PlayerVotedEvent(Id, target.Id));
    }
}