using MafiCo.Domain.ValueObjects;

namespace MafiCo.Application.Interfaces;

public interface IPlayerBrain {
    public void InformAboutRole(Role role);
    public Task<string> ChooseTargetAsync();
}