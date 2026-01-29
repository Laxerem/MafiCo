namespace MafiCo.Application.Interfaces;

public interface IPlayerBrain {
    public Task<string> ChooseTargetAsync();
    
}