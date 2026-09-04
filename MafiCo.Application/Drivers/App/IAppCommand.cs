using MediatR;

namespace MafiCo.Application.Drivers.App;

public interface IAppCommand<TResult> : IRequest<TResult> {}
public interface IAppCommand : IRequest {}