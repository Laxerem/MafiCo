namespace MafiCo.Application.Interfaces.Stores;

public interface IUserStore {
    void SetUser(Guid userId);
    Guid? GetUserId();
}