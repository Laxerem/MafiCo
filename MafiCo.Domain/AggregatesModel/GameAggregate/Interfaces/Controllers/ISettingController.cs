namespace MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;

public interface ISettingController : IController {
    void Setup(IEnumerable<Guid> playerIds, int mafiaCount);
}