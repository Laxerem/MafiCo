using MafiCo.Domain.Abstractions;
using MafiCo.Domain.Events;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Entities;

public class Mafia : Player {
    public bool isActive { get; private set; }

    public Mafia(string name, Role role) : base(name, role) {
        isActive = false;
    }

    public void SelectTarget(string targetName) {
        AddNotification(new MafiaSelectTargetEvent(targetName));
    }
}