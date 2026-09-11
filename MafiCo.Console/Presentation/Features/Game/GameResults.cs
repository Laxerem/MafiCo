using MafiCo.Application.Notifications.GameNotifications;
using MafiCo.Domain.DTOs;
using MafiCo.Infrastructure.DTOs;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// UI-объект финального экрана: исход для игрока, составы победителей и
/// проигравших с раскрытыми ролями.
/// </summary>
public sealed class GameResults {
    private readonly Guid _selfId;

    public GameResults(Guid selfId) {
        _selfId = selfId;
    }

    public IRenderable Render(GameFinishedNotification result, IReadOnlyList<PublicPlayerInfo> players) {
        var names = players.ToDictionary(player => player.Id, player => player.Name);

        return new Rows(
            BuildOutcome(result),
            BuildSide("Победители", "green", result.Winners, names),
            BuildSide("Проигравшие", "red", result.Losers, names));
    }

    private IRenderable BuildOutcome(GameFinishedNotification result) {
        var won = result.Winners.Any(player => player.Id == _selfId);
        var lost = result.Losers.Any(player => player.Id == _selfId);

        var text = (won, lost) switch {
            (true, _) => "[bold green]Вы победили[/]",
            (_, true) => "[bold red]Вы проиграли[/]",
            _ => "[bold]Игра окончена[/]",
        };

        return new Markup(text);
    }

    private IRenderable BuildSide(
        string header,
        string color,
        IReadOnlyList<PlayerInfo> side,
        IReadOnlyDictionary<Guid, string> names) {
        IRenderable content = side.Count == 0
            ? new Markup("[grey]—[/]")
            : new Rows(side.Select(player => FormatPlayer(player, names)));

        return new Panel(content)
            .Header($"[{color}]{header}[/]")
            .Expand();
    }

    private Markup FormatPlayer(PlayerInfo player, IReadOnlyDictionary<Guid, string> names) {
        var role = player.Role.ToString();
        var name = names.TryGetValue(player.Id, out var found) ? Markup.Escape(found) : "[grey]?[/]";
        var self = player.Id == _selfId ? " [grey](вы)[/]" : string.Empty;

        return new Markup($"[bold]{role}[/] — {name}{self}");
    }
}
