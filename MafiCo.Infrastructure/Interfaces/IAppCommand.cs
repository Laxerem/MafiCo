using MediatR;

namespace MafiCo.Infrastructure.Interfaces;

public interface IAppCommand : IRequest {}
public interface IAppCommand<TRequest> : IRequest<TRequest> {}