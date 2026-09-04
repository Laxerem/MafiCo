using MafiCo.Application.Abstractions;
using MafiCo.Infrastructure.MediatR.Game.Commands;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Game.Handlers;

public class StartGameHandler : IRequestHandler<StartGameCommand, PlayerInterface> {
    
    public Task<PlayerInterface> Handle(StartGameCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}