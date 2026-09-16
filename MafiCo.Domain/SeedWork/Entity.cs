namespace MafiCo.Domain.SeedWork;

public abstract class Entity {
    public readonly Guid Id;

    public Entity(Guid id) {
        Id = id;
    }
}