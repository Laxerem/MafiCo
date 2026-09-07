using MafiCo.Application.Notifications.GameNotifications;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// UI-объект чата: единая хронологическая лента сообщений игроков и игровых событий.
/// Сообщения игрока подсвечиваются одним цветом, игровые события — другим.
/// </summary>
public sealed class GameChat {
    private enum EntryKind {
        OtherPlayer,
        Self,
        GameEvent,
    }

    private readonly List<(EntryKind Kind, string Text)> _entries = new();
    private readonly string _selfName;

    public GameChat(string selfName) {
        _selfName = selfName;
    }

    public void AppendMessage(PlayerMessageNotification message) {
        var kind = string.Equals(message.Name, _selfName, StringComparison.Ordinal)
            ? EntryKind.Self
            : EntryKind.OtherPlayer;

        _entries.Add((kind, $"{message.Name}: {message.Message}"));
    }

    public void AppendGameEvent(string text) {
        _entries.Add((EntryKind.GameEvent, text));
    }

    public IRenderable Render() {
        IRenderable content = _entries.Count == 0
            ? new Markup("[grey]Сообщений пока нет[/]")
            : new Rows(_entries.Select(FormatEntry));

        return new Panel(content)
            .Header("Чат")
            .Expand();
    }

    private static Markup FormatEntry((EntryKind Kind, string Text) entry) {
        var text = Markup.Escape(entry.Text);
        return entry.Kind switch {
            EntryKind.Self => new Markup($"[green]{text}[/]"),
            EntryKind.GameEvent => new Markup($"[yellow]* {text}[/]"),
            _ => new Markup(text),
        };
    }
}
