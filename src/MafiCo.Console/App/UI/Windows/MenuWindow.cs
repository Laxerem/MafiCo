using MafiCo.Console.App.Exceptions;
using MafiCo.Console.App.UI.Common;
using MafiCo.Console.App.UI.Events;
using MafiCo.Console.App.UI.Windows.Game;
using MafiCo.Domain.Exceptions;
using Spectre.Console;

namespace MafiCo.Console.App.UI.Windows;

public class MenuWindow : UiWindow {
    public override event Action<UiWindow> OnSwitchWindow;
    protected override event Action<UiEvent> OnEvent;

    public override async Task Show() {
        var choice = await WaitChoice("MafiCo", ["Играть", "Выйти"]);
        switch (choice) {
            case "Играть":
                OnSwitchWindow?.Invoke(new StartGameWindow());
                break;
            case "Выйти":
                throw new GameClosedException("Мафия не ждёт...");
        }
    }
}