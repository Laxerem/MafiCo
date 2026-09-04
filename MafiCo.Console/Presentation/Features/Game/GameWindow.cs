namespace MafiCo.Console.Presentation.Features.Game;

// public class GameWindow : Window {
//     private readonly GetPlayerController _getPlayerController;
//     public GameWindow(GetPlayerController getPlayerController) {
//         _getPlayerController = getPlayerController;
//     }
//     public override Task Show() {
//         AnsiConsole.Console.Write(new FigletText("MafiCo"));
//         var processor = _getPlayerController.Wait();
//         var role = processor.CheckRole();
//         AnsiConsole.Console.Write($"Роль: {role.ToString()}");
//         return Task.CompletedTask;
//     }
// }
