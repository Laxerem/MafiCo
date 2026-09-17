using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Game.Notifications;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// UI-объект финального экрана: исход для игрока и составы победителей и
/// проигравших.
/// </summary>
public sealed class GameResults {
    private readonly Guid _selfId;

    public GameResults(Guid selfId) {
        _selfId = selfId;
    }

    public IRenderable Render(GameFinishedNotification result) {
        return new Rows(
            BuildOutcome(result),
            BuildSide("Победители", "green", result.Winners),
            BuildSide("Проигравшие", "red", result.Losers));
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

    private IRenderable BuildSide(string header, string color, IReadOnlyList<ResultPlayerInfo> side) {
        IRenderable content = side.Count == 0
            ? new Markup("[grey]—[/]")
            : new Rows(side.Select(FormatPlayer));

        return new Panel(content)
            .Header($"[{color}]{header}[/]")
            .Expand();
    }

    private Markup FormatPlayer(ResultPlayerInfo player) {
        var role = player.Role.ToString();
        var name = Markup.Escape(player.Name);
        var self = player.Id == _selfId ? " [grey](вы)[/]" : string.Empty;
        var status = player.IsAlive ? string.Empty : " [grey](погиб)[/]";

        return new Markup($"[bold]{role}[/] — {name}{self}{status}");
    }
}
