using MafiCo.Application.Interfaces;
using MafiCo.Domain.Events;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Infrastracture.Brains;

public class HumanBrain : IPlayerBrain {
    public event Func<IGameEvent, Task> Actions;
    
    public HumanBrain() {
        
    }

    public Task<string> ChooseTargetAsync() {
        throw new NotImplementedException();
    }

    public async Task InformAboutRole(Role role) {
        await Actions.Invoke(new RoleIsDetermineEvent("", role));
    }
}