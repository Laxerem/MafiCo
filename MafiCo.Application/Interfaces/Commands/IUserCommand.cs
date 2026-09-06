using MediatR;

namespace MafiCo.Application.Interfaces.Commands;

public interface IUserCommand : IRequest {}
public interface IUserCommand<TRequest> : IRequest<TRequest> {}