using MafiCo.Domain.Events;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Abstractions;

public abstract class Player : Entity {
    public string Name { get; protected set; }
    public Role Role { get; protected set; }
    public bool IsAlive { get; private set; }

    public Player(string name, Role role) {
        Name = name;
        Role = role;
        IsAlive = true;
    }
    
    internal void Kill() {
        IsAlive = false;
    }

    public void Vote(string username) {
        AddNotification(new PlayerVotingEvent(username));
    }
}