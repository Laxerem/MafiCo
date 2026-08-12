using MafiCo.Domain.Events.Profile;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.ProfileAggregate;

public class Profile : Entity, IAggregateRoot {
    public string Name {get; private set;}
    public int VictoriesCount { get; private set; }
    public int DefeatsCount { get; private set; }

    private Profile(string name) :  base(Guid.NewGuid()) {
        Name = name;
        AddNotification(new ProfileCreatedEvent(this));
    }

    public void ChangeName(string newName) {
        Validate(newName);
        Name = newName;
        AddNotification(new UsernameChangedEvent(Id, newName));
    }

    public static Profile Create(string name) {
        Validate(name);
        return new Profile(name);
    }

    private static void Validate(string playerName) {
        if (playerName.Length < 3 || playerName.Length > 15) {
            throw new ProfileException("The name must be between 3 and 15 characters.");
        }
    }
}