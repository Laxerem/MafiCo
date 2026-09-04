using MediatR;

namespace MafiCo.Application.Interfaces.Commands;

public interface IAppCommand : IRequest {}
public interface IAppCommand<TRequest> : IRequest<TRequest> {}