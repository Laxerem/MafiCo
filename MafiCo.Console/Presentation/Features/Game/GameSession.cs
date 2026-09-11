using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Notifications.GameNotifications;
using MafiCo.Domain.DTOs;
using MafiCo.Infrastructure.DTOs;
using MafiCo.Infrastructure.MediatR.Game.Commands;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// Игровой экран: крутит цикл "вычитать события -> перерисовать -> дать игроку сходить".
/// Полудуплекс: пока игрок вводит сообщение, входящие события копятся в канале
/// и показываются после нажатия Enter. Завершается по <see cref="GameFinishedNotification"/>.
/// </summary>
public sealed class GameSession {
    private readonly PlayerContext _context;
    private readonly IMediator _mediator;
    private readonly Guid _selfId;
    private readonly GameChat _chat;
    private readonly GamePlayers _players;
    private readonly GameResults _results;

    private IReadOnlyList<PublicPlayerInfo> _playerList = [];
    private GameFinishedNotification? _finished;

    public GameSession(PlayerContext context, ProfileInfo me, IMediator mediator) {
        _context = context;
        _mediator = mediator;
        _selfId = me.Id;
        _chat = new GameChat(me.Name);
        _players = new GamePlayers(me.Id);
        _results = new GameResults(me.Id);
    }

    public async Task RunAsync() {
        var dirty = true;
        while (true) {
            if (DrainNotifications()) {
                dirty = true;
            }

            if (_finished is not null) {
                await ShowResultsAsync(_finished);
                return;
            }

            if (dirty) {
                await RefreshPlayersAsync();
                Render();
                dirty = false;
            }

            var view = ControllerViewFactory.Create(_context.Controller, _playerList, _selfId);
            if (view is null) {
                await Task.Delay(500);
                continue;
            }

            await view.RunTurnAsync();
            dirty = true;
        }
    }

    private async Task ShowResultsAsync(GameFinishedNotification result) {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("MafiCo"));
        AnsiConsole.Write(_results.Render(result, _playerList));

        await AnsiConsole.PromptAsync(
            new TextPrompt<string>("[grey]Нажмите Enter, чтобы вернуться в меню[/]").AllowEmpty());
    }

    private async Task RefreshPlayersAsync() {
        _playerList = await _mediator.Send(new GetPlayersCommand());
        _players.Update(_playerList);
    }

    private bool DrainNotifications() {
        var changed = false;
        while (_context.EventsReader.TryRead(out var notification)) {
            changed = true;

            switch (notification) {
                case PlayerMessageNotification message:
                    _chat.AppendMessage(message);
                    break;
                case GameFinishedNotification finished:
                    _finished ??= finished;
                    break;
                default:
                    _chat.AppendGameEvent(NotificationBuilder.Build(notification));
                    break;
            }
        }

        return changed;
    }

    private void Render() {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("MafiCo"));
        AnsiConsole.Write(_players.Render());
        AnsiConsole.Write(_chat.Render());
    }
}
