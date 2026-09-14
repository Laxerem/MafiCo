using MafiCo.Application.Game.Commands;
using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Console.Presentation.Features.Game.ControllerViews;
using MafiCo.Domain.DTOs;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Game;

/// <summary>
/// Игровой экран: крутит цикл "вычитать события -> перерисовать -> дать игроку сходить".
/// Ход игрока прерывается сам, как только в канале появляется новое событие, — экран
/// перерисовывается и ход запрашивается заново, без ручного действия игрока.
/// Завершается по <see cref="GameFinishedNotification"/>.
/// </summary>
public sealed class GameSession {
    private readonly IPlayerSession _session;
    private readonly ISender _mediator;
    private readonly Guid _selfId;
    private readonly GameChat _chat;
    private readonly GamePlayers _players;
    private readonly GameResults _results;

    private IReadOnlyList<PublicPlayerInfo> _playerList = [];
    private GameFinishedNotification? _finished;

    public GameSession(IPlayerSession session, ProfileInfo me, IMediator mediator) {
        _session = session;
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

            var view = ControllerViewFactory.Create(_session.Controller, _playerList, _selfId);
            if (view is null) {
                await Task.Delay(500);
                continue;
            }

            await RunInterruptibleTurnAsync(view);
            dirty = true;
        }
    }

    /// <summary>
    /// Проводит ход контроллера, прерывая его, как только в канал приходит новое событие:
    /// без этого пока игрок выбирает вариант или печатает сообщение, входящие события
    /// копились бы в канале и оставались невидимыми до завершения хода.
    /// </summary>
    private async Task RunInterruptibleTurnAsync(IControllerView view) {
        using var interrupt = new CancellationTokenSource();
        var watcher = WatchForNotificationsAsync(interrupt);

        try {
            await view.RunTurnAsync(interrupt.Token);
        } catch (OperationCanceledException) when (interrupt.IsCancellationRequested) {
            // Пришло новое событие — ход прервался сам, экран перерисуется и запросит ход заново.
        } finally {
            interrupt.Cancel();
            await watcher;
        }
    }

    private async Task WatchForNotificationsAsync(CancellationTokenSource interrupt) {
        try {
            if (await _session.EventsReader.WaitToReadAsync(interrupt.Token)) {
                interrupt.Cancel();
            }
        } catch (OperationCanceledException) {
            // Ход завершился сам — дальше ждать не нужно.
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
        while (_session.EventsReader.TryRead(out var notification)) {
            changed = true;

            switch (notification) {
                case PlayerMessageNotification message:
                    _chat.AppendMessage(message);
                    break;
                case GameFinishedNotification finished:
                    _finished ??= finished;
                    break;
                case ControllerChangedNotification:
                    // Технический сигнал: PlayerView.Controller уже обновлён, тут только
                    // будим цикл — он прервёт текущий ход и перечитает новый контроллер.
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
