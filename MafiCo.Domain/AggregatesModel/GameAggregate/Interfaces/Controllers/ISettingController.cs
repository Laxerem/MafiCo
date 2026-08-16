namespace MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;

public interface ISettingController : IController {
    void Setup(int mafiaCount);
}