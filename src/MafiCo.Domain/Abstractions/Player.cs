using MafiCo.Domain.Entities;
using MafiCo.Domain.Events;
using MafiCo.Domain.Events.Player;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Abstractions;

public abstract class Player : Entity {
    public string Name { get; protected set; }
    public bool IsAlive { get; private set; }
    private Role _role;

    public Player(string name, Role role) {
        Name = name;
        IsAlive = true;
        _role = role;
    }
    
    internal void Kill() {
        IsAlive = false;
    }

    public void Vote(string username) {
        if (IsAlive == true) {
            AddNotification(new PlayerVotingEvent(username));   
        }
    }

    public void Say(string text) {
        if (IsAlive == true) {
            AddNotification(new PlayerSayEvent(Name, text));
        }
    }
}