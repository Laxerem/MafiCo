using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Windows.Lobby.Events;

namespace MafiCo.Console.Presentation.Windows.Game;

public class GameWindow : Window {
    public GameWindow() {
        
    }
    
    public async override Task Show() {
        await UseAsync(new StartGame());
    }
}