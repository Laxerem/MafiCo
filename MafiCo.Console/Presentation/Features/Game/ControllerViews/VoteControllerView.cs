using MafiCo.Infrastructure.Controllers;
using MafiCo.Infrastructure.DTOs;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game.ControllerViews;

/// <summary>
/// Управление игрой в фазе голосования: игрок выбирает цель среди живых игроков,
/// выбор уходит как голос. Соответствует <see cref="VoterController"/>.
/// </summary>
internal sealed class VoteControllerView : IControllerView {
    private readonly VoterController _controller;
    private readonly IReadOnlyList<PublicPlayerInfo> _players;
    private readonly Guid _selfId;

    public VoteControllerView(VoterController controller, IReadOnlyList<PublicPlayerInfo> players, Guid selfId) {
        _controller = controller;
        _players = players;
        _selfId = selfId;
    }

    public async Task RunTurnAsync() {
        var options = _players
            .Where(player => player.IsAlive && player.Id != _selfId)
            .Select(player => new VoteOption(Markup.Escape(player.Name), player.Id))
            .Prepend(new VoteOption("[grey]— обновить экран —[/]", null))
            .ToList();

        var choice = await AnsiConsole.PromptAsync(
            new SelectionPrompt<VoteOption>()
                .Title("[red]Кого казнить?[/]")
                .UseConverter(option => option.Label)
                .AddChoices(options));

        if (choice.TargetId is { } targetId) {
            await _controller.MakeVote(targetId);
        }
    }

    private sealed record VoteOption(string Label, Guid? TargetId);
}
