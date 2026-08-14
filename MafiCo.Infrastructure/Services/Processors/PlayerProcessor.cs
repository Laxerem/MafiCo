using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Domain.Entities.Players;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.Services.Processors;

public abstract class PlayerProcessor : IProcessor {
    protected readonly Role _role;
    protected readonly Guid _playerId;
    protected readonly IPlayerController _controller;
    
    public PlayerProcessor(Guid playerId, Role role, IPlayerController controller) {
        _playerId = playerId;
        _role = role;
        _controller = controller;
    }

    public void Vote(Guid targetId) {
        _controller.Vote(_playerId, targetId);
    }

    public Role CheckRole() => _role;
}