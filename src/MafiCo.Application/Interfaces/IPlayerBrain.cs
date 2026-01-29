using MafiCo.Domain.ValueObjects;

namespace MafiCo.Application.Interfaces;

public interface IPlayerBrain {
    public Task<string> ChooseTargetAsync();
    public Task InformAboutRole(Role role);
}