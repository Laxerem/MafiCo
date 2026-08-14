namespace MafiCo.Infrastructure.Interfaces.Store;

public interface IUserStore {
    void SetUser(Guid userId);
    Guid? GetUserId();
}