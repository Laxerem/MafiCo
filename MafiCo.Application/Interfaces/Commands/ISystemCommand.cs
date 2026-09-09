using MediatR;

namespace MafiCo.Application.Interfaces.Commands;

public interface ISystemCommand : IRequest {}
public interface ISystemCommand<TRequest> : IRequest<TRequest> {}