using MafiCo.Application.Game.DTOs;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// UI-объект списка игроков: кто участвует в матче и кто ещё жив.
/// </summary>
public sealed class GamePlayers {
    private readonly PlayerId _selfId;
    private IReadOnlyList<PublicPlayerInfo> _players = [];

    public GamePlayers(PlayerId selfId) {
        _selfId = selfId;
    }

    public void Update(IReadOnlyList<PublicPlayerInfo> players) {
        _players = players;
    }

    public IRenderable Render() {
        IRenderable content = _players.Count == 0
            ? new Markup("[grey]Список игроков пуст[/]")
            : new Rows(_players.Select(FormatPlayer));

        return new Panel(content)
            .Header("Игроки")
            .Expand();
    }

    private Markup FormatPlayer(PublicPlayerInfo player) {
        var name = Markup.Escape(player.Name);
        var self = player.Id == _selfId ? " [grey](вы)[/]" : string.Empty;

        return player.IsAlive
            ? new Markup($"[green]●[/] {name}{self}")
            : new Markup($"[red]●[/] [strikethrough]{name}[/]{self}");
    }
}
