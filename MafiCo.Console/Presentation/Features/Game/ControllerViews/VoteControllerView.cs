using MafiCo.Application.Game.Controllers;
using MafiCo.Application.Game.DTOs;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game.ControllerViews;

/// <summary>
/// Управление игрой в фазе голосования: игрок выбирает цель среди живых игроков,
/// выбор уходит как голос. Соответствует <see cref="VoterController"/>.
/// </summary>
internal sealed class VoteControllerView : IControllerView {
    private readonly VoterController _controller;
    private readonly IReadOnlyList<PublicPlayerInfo> _players;
    private readonly PlayerId _selfId;

    public VoteControllerView(VoterController controller, IReadOnlyList<PublicPlayerInfo> players, PlayerId selfId) {
        _controller = controller;
        _players = players;
        _selfId = selfId;
    }

    public async Task RunTurnAsync(CancellationToken cancellationToken) {
        var options = _players
            .Where(player => player.IsAlive && player.Id != _selfId)
            .Select(player => new VoteOption(Markup.Escape(player.Name), player.Id))
            .ToList();

        if (options.Count == 0) {
            return;
        }

        var choice = await AnsiConsole.PromptAsync(
            new SelectionPrompt<VoteOption>()
                .Title("[red]Кого казнить?[/]")
                .UseConverter(option => option.Label)
                .AddChoices(options),
            cancellationToken);

        await _controller.MakeVote(choice.TargetId);
    }

    private sealed record VoteOption(string Label, PlayerId TargetId);
}
