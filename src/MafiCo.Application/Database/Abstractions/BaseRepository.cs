namespace MafiCo.Application.Database.Abstractions;

public abstract class BaseRepository {
    public abstract Task InitializeTableAsync();
}