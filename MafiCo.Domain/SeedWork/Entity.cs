namespace MafiCo.Domain.SeedWork;

public abstract class Entity<T> {
    public readonly T Id;

    public Entity(T id) {
        Id = id;
    }
}