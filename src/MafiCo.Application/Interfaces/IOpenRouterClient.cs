namespace MafiCo.Application.Interfaces;

public interface IOpenRouterClient {
    public Task<string> GetResponseAsync(string request, string modelName);
}