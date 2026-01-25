using MafiCo.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace MafiCo.Application.Database.Repositories;

public class HumanProfilesRepository {
    private readonly string _filePath;
    
    public HumanProfilesRepository(string databaseFilePath) {
        _filePath = databaseFilePath;
    }

    public async Task InitializeTableAsync() {
        using var connection = new SqliteConnection($"Data Source={_filePath}");
        await connection.OpenAsync();
        using var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = $"""
                                          CREATE TABLE IF NOT EXISTS HumanProfiles (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              Username TEXT NOT NULL UNIQUE
                                          )
                                          """;
        await createTableCommand.ExecuteNonQueryAsync();
    }

    public async Task AddAsync(string username) {
        using var connection = new SqliteConnection($"Data Source={_filePath}");
        await connection.OpenAsync();
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO HumanProfiles (Username) VALUES ($username)";
        insertCommand.Parameters.AddWithValue("$username", username);
        await insertCommand.ExecuteNonQueryAsync();
    }

    public async Task<List<HumanProfile>> GetAllAsync() {
        using var connection = new SqliteConnection($"Data Source={_filePath}");
        await connection.OpenAsync();
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = $"SELECT * FROM HumanProfiles";
        
        using var reader = await selectCmd.ExecuteReaderAsync();
        List<HumanProfile> result = new List<HumanProfile>();
        while (await reader.ReadAsync()) {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            result.Add(new HumanProfile(id, name));
        }
        return result;
    }
}