using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Notifications.GameNotifications;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// Игровой экран: крутит цикл "вычитать события -> перерисовать -> дать игроку сходить".
/// Полудуплекс: пока игрок вводит сообщение, входящие события копятся в канале
/// и показываются после нажатия Enter.
/// </summary>
public sealed class GameSession {
    private readonly PlayerContext _context;
    private readonly GameChat _chat;

    public GameSession(PlayerContext context, string selfName) {
        _context = context;
        _chat = new GameChat(selfName);
    }

    public async Task RunAsync() {
        var dirty = true;
        while (true) {
            if (DrainNotifications()) {
                dirty = true;
            }

            if (dirty) {
                Render();
                dirty = false;
            }

            var view = ControllerViewFactory.Create(_context.Controller);
            if (view is null) {
                await Task.Delay(500);
                continue;
            }

            await view.RunTurnAsync();
            dirty = true;
        }
    }

    private bool DrainNotifications() {
        var changed = false;
        while (_context.EventsReader.TryRead(out var notification)) {
            changed = true;

            if (notification is PlayerMessageNotification message) {
                _chat.AppendMessage(message);
                continue;
            }

            _chat.AppendGameEvent(NotificationBuilder.Build(notification));
        }

        return changed;
    }

    private void Render() {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("MafiCo"));
        AnsiConsole.Write(_chat.Render());
    }
}
