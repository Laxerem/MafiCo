# CLAUDE.md — MafiCo

MafiCo is a single-process **terminal Mafia game** in .NET 9, meant as a sandbox for testing LLM models as players. Players join a match, get roles (Mafia/Citizen) and play Day/Night phases in a Spectre.Console TUI. Profiles, bots, LLM settings, match records and win stats are persisted to SQLite via EF Core; the in-progress match state lives in memory.

The project is under active development and **not feature-complete** — see "Known incomplete areas" before assuming any flow works end-to-end.

## Key docs

| File | When to read |
|------|-------------|
| `README.md` | Product overview, build/run, domain and controller diagrams (`assets/docs/`) |

---

## Tech stack

- **.NET 9** (`net9.0`), SDK pinned by `global.json` to `9.0.100` (rollForward `latestMinor`)
- **MediatR 14.2.0** — CQRS commands + notification dispatch. Referenced directly by Domain, Application and Console
- **EF Core 9.0.0 + SQLite** — persistence; schema `mafico`
- **Spectre.Console 0.57.2** — the entire UI
- **Microsoft.Extensions.Hosting 10.0.10** — generic host / DI (note: 10.x, unlike EF Core 9.x)
- `Nullable` and `ImplicitUsings` enabled in all four projects. No `.editorconfig`, no analyzers, no `Directory.Build.props`

---

## Directory structure

```
MafiCo.Domain/           # Pure domain model, no project refs
├── AggregatesModel/     # One folder per aggregate: Game, Bot, Profile, Llm (+ repository interface)
│   └── GameAggregate/   #   Game.cs root + Entities/ (Player, Voting) + Events/ (IDomainEvents) + Items/ (enums)
├── SeedWork/            # Entity, IAggregateRoot, IDomainEvent, DomainException
├── ValueObjects/        # Phase/PhaseType — DEAD, see Don't
├── DTOs/                # Cross-aggregate read records
└── Exceptions/          # DomainException subclasses per aggregate

MafiCo.Application/      # → Domain. All use cases (CQRS) + the in-memory live match runtime
├── Bot/, Llm/, Profile/ # Commands/ + Handlers/ per feature (Bot nests Handlers/ under Commands/)
├── Game/
│   ├── Controllers/     #   IPlayerController implementations (DefaultController = chat, VoterController = vote)
│   ├── Notifications/   #   IGameNotification records shown to players
│   ├── Mediator/        #   Access/ (GamePublisher, PlayerSender), Commands/ + Handlers/ reachable from the UI
│   │   └── Internal/    #     Commands/Events/Handlers not reachable from the UI
│   │       └── Handlers/AggregateSource/  # handlers of Domain IDomainEvents
│   └── GameContext, GameWorker, PlayerProcessor, PlayerView  # live match objects
├── Interfaces/          # IUnitOfWork, IPlayerController, notify/publisher/sender/store contracts
└── ApplicationExtension.cs  # AddApplication(): GameContext + AddMediatR()

MafiCo.Infrastructure/   # → Application, Domain. Persistence only, no CQRS code
├── Persistence/         # ApplicationContext, Configurations/, Repositories/, UnitOfWork
├── Migrations/
└── MediatorExtension.cs # DispatchDomainEventsAsync — drains Entity.Notifications into mediator.Publish

MafiCo.Console/          # → Domain, Infrastructure. Composition root + TUI
├── Program.cs / App.cs  # host build, DI wiring, startup migrations, first screen selection
├── Presentation/        # Base/ (Window), Features/<Screen>/, Styles/, UserInterface.cs (router)
├── Configuration/       # appconfig.json read/write (current user Id)
└── System/Stores/       # UserStore (IUserStore)
```

---

## Key patterns

### Dependency rule
Strictly inward: `Console → Infrastructure → Application → Domain`. Interfaces live in the inner layer, implementations in the outer one; repository interfaces sit next to their aggregate. `MafiCo.Console.csproj` does **not** reference Application directly — its types are visible transitively.

### Match lifecycle
`StartGameHandler` creates `Game` from all profiles, saves it, initializes the singleton `GameContext` (one `PlayerProcessor` per player) and starts a `GameWorker` — `new`-ed per match, not in DI. `GameWorker` assigns roles and loops `NextPhase()`, publishing `RoleAssignedEvent`/`PhaseChangedEvent` via `IGamePublisher`. `PhaseChangedHandler` paces the phase with delays and swaps controllers: Day — chat, then vote for everyone; Night — citizens' processors stopped, mafia gets `VoterController`.

### Two event pipelines — pick the right one
- `IGameEvent` (Application) — internal game moments from `GameWorker`, handled under `Game/Mediator/Internal/Handlers/`.
- `IDomainEvent` (Domain, `Entity.AddNotification`) — dispatched only inside `UnitOfWork.SaveEntitiesAsync`, handled under `Internal/Handlers/AggregateSource/`.

Both must translate into an `IGameNotification`, sent through `GameContext`; `PlayerProcessor` pushes it into the per-player channel on `PlayerView`, drained by `GameSession` in the UI.

### Persistence of a match
`SaveEntitiesAsync` dispatches domain events **before** `SaveChangesAsync`, so entity changes made by `IDomainEvent` handlers (e.g. `GameFinishedHandler` → `Profile.RegisterWin()`) are persisted by the enclosing call — the handlers don't save themselves. `PhaseChangedHandler` calls `SaveEntitiesAsync` on every phase. Only `Game`'s `Id`/`StartedAt`/`FinishedAt` are mapped; players, roles, votes and phase are not.

### CQRS via MediatR
Every use case is a command `record` + handler class in `MafiCo.Application`. MediatR is registered once in `AddApplication()`, scanning the Application assembly. The UI never touches aggregates — it sends commands via `IMediator`/`IPlayerSender` and reads notifications from the channel.

### Screen routing
`Window` subclasses raise `OnSwitchWindow`; `UserInterface` resolves the target from DI. `AddUi()` reflection-registers all non-abstract `Window` types — **a new screen needs no DI registration**. `GameSession` is outside this router and owns its render loop.

### Database
Migrations are applied at startup (`ApplyMigrationsAsync` in `Program.cs`). The SQLite path is hardcoded in `ApplicationContext` as `../../../mafico.db`, relative to the **working directory** — launching from different folders opens different databases.

---

## Conventions

- **Brace style is K&R** (opening brace on the declaration line), not Allman.
- File-scoped namespaces. Classic constructors only; no primary constructors on classes.
- `record` for events, notifications, commands and DTOs; `class` for aggregates, entities, handlers, services.
- Aggregates: `private` parameterless ctor for EF + a public ctor or `static Create(...)` factory.
- Private fields `_camelCase`. Interfaces `I`-prefixed (`WindowData` is an exception).
- Errors are **exceptions, not result types**. Domain throws `DomainException` subclasses.
- UI strings and Presentation-layer XML docs are **Russian**; identifiers are English.
- Folder ≠ namespace in places (`Presentation/Styles/` → `...Presentation.Extensions`, `SeedWork/IAggregateRoot.cs` → `...Domain.Interfaces`, `Interfaces/Mediator/Notifications/` → `...Interfaces.Notifications`, `Game/Mediator/Commands/` → `...Game.Commands`). Grep by type name, not path.
- Repository interfaces are inconsistently sync/async — follow the aggregate you are extending.

### Don't

- Don't send a domain type to the UI — translate every event into an `IGameNotification`.
- Don't extend `Domain/ValueObjects/Phase.cs`/`PhaseType.cs`; the live phase model is `GameAggregate/Items/GamePhase`.
- Don't add MediatR commands/handlers under `MafiCo.Infrastructure`.
- Don't raise an `IDomainEvent` expecting delivery without a `SaveEntitiesAsync` call afterwards.
- Don't add `EnsureCreated` or tell users to run `dotnet ef database update` — startup migrations cover it.
- Don't add a connection string to config expecting it to be used; changing the hardcoded DB path is a deliberate refactor, not a drive-by fix.
- Don't uncomment `IGameStore`/`GameStore` or the bot branch in `CreateProcessorHandler` as a side effect of unrelated work.

### Known incomplete areas

- Bot/LLM play is stubbed: `CreateProcessorHandler`'s bot branch is commented out.
- In-progress matches can't be restored — only the `Game` record and win counts are persisted.
- Losses are not tracked: `Profile.DefeatsCount` exists but nothing increments it. `GameFinishedHandler` has an unassigned `_unitOfWork` field.
- `GameWorker.RunAsync` doesn't observe the `Work` task, and `Work` loops `while (true)`; after the game finishes it ends only via an unobserved exception.
- `NotificationBuilder.Build` throws on unknown notification types — extend its switch whenever you add a notification.

---

## Post-change checklist

```bash
dotnet build MafiCo.sln                                    # no analyzers — a clean build is the only mechanical check
dotnet run --project MafiCo.Console/MafiCo.Console.csproj  # smoke-test the TUI

# only when the EF model changed (applied automatically on next start):
dotnet ef migrations add <Name> --project MafiCo.Infrastructure --startup-project MafiCo.Console
```

There are **no automated tests and no CI**. Anything touching the game loop or notification fan-out must be verified by running the app.
