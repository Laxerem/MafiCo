using MafiCo.Application.Interfaces;

namespace MafiCo.Infrastracture.Brains;

public class HumanBrain : IPlayerBrain {
    public HumanBrain() {
        
    }

    public Task<string> ChooseTargetAsync() {
        throw new NotImplementedException();
    }
}