using MafiCo.Console.Presentation.Base;

namespace MafiCo.Console.Presentation.Windows.Lobby.UseCases;

public record CreateLlm(string ModelName, string Url, string ApiKey) : UseCase;