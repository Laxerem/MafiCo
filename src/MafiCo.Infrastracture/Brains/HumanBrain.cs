using MafiCo.Application.Interfaces;
using MafiCo.Domain.Events;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;
using MafiCo.Infrastracture.Events;

namespace MafiCo.Infrastracture.Brains;

public class HumanBrain : IPlayerBrain {
    
    public HumanBrain() {
        
    }

    public Task<string> ChooseTargetAsync() {
        throw new NotImplementedException();
    }
}