using MediatR;

namespace MafiCo.Application.Interfaces.Commands;

public interface IPlayerCommand : IRequest {}
public interface IPlayerCommand<TResponse> : IRequest<TResponse> {}