using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MafiCo.Infrastructure.Persistence.Interceptors;

public sealed class DbRequestsInterceptor : DbCommandInterceptor {
    private readonly ILogger _logger = Log.ForContext<DbRequestsInterceptor>();

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result,
        CancellationToken cancellationToken = new CancellationToken()) {
        _logger.Information("Handle a request: {command}", command.CommandText);
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }
}
