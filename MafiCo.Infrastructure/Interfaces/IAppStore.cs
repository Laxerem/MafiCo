namespace MafiCo.Infrastructure.Interfaces;

public interface IAppStore {
     void SetUser(Guid userId);
     Guid? GetUserId();
}