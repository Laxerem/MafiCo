namespace MafiCo.Application.Interfaces;

public interface IUnitOfWork {
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
