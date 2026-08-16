using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.Services.Processors;

public class SettingProcessor : IProcessor {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISettingController _controller;
    private int _mafiaCount = 1;
    
    public SettingProcessor(ISettingController controller, IUnitOfWork unitOfWork) {
        _controller = controller;
        _unitOfWork = unitOfWork;
    }

    public void SetupMafiaCount(int mafiaCount) {
        _mafiaCount = mafiaCount;
    }

    public async Task StartGame() {
        _controller.Setup(_mafiaCount);
        await _unitOfWork.SaveEntitiesAsync();
    }
}