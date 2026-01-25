using MafiCo.Application.Database.Abstractions;
using MafiCo.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace MafiCo.Application.Database.Repositories;

public class BotProfilesRepository : BaseRepository {
    private readonly string _filePath;
    
    public BotProfilesRepository(string databaseFilePath) {
        _filePath = databaseFilePath;
    }

    public override async Task InitializeTableAsync() {
        using var connection = new SqliteConnection($"Data Source={_filePath}");
        await connection.OpenAsync();
        using var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = $"""
                                          CREATE TABLE IF NOT EXISTS BotProfiles (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              Username TEXT NOT NULL UNIQUE,
                                              Modelname TEXT NOT NULL,
                                              SystemPrompt TEXT NOT NULL
                                          )
                                          """;
        await createTableCommand.ExecuteNonQueryAsync();
    }

    public async Task AddAsync(string username, string modelname, string systemPrompt) {
        using var connection = new SqliteConnection($"Data Source={_filePath}");
        await connection.OpenAsync();
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO HumanProfiles (Username, Modelname, SystemPrompt) VALUES ($username, $modelname, $systemPrompt)";
        insertCommand.Parameters.AddWithValue("$username", username);
        insertCommand.Parameters.AddWithValue("$modelname", modelname);
        insertCommand.Parameters.AddWithValue("$systemPrompt", systemPrompt);
        await insertCommand.ExecuteNonQueryAsync();
    }

    public async Task<List<BotProfile>> GetAllAsync() {
        using var connection = new SqliteConnection($"Data Source={_filePath}");
        await connection.OpenAsync();
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = $"SELECT * FROM BotProfiles";
        
        using var reader = await selectCmd.ExecuteReaderAsync();
        List<BotProfile> result = new ();
        while (await reader.ReadAsync()) {
            var id = reader.GetInt32(0);
            var username = reader.GetString(1);
            var modelName = reader.GetString(2);
            var systemPrompt = reader.GetString(3);
            result.Add(new BotProfile(id, username, modelName, systemPrompt));
        }
        return result;
    }
}