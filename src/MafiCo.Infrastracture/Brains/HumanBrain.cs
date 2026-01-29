using MafiCo.Application.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Infrastracture.Brains;

public class HumanBrain : IPlayerBrain {
    public Action<> Actions;
    
    public HumanBrain() {
        
    }

    public Task<string> ChooseTargetAsync() {
        throw new NotImplementedException();
    }

    public Task InformAboutRole(Role role) {
        return Task.CompletedTask;
    }
}