namespace MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;

public interface IPlayerController : IController {
    void Vote(Guid voterId, Guid targetId);
}