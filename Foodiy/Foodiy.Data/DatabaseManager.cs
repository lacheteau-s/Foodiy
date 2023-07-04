using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace Foodiy.Data;

public class DatabaseManager : IDatabaseManager
{
    private readonly DbProviderFactory _dbProviderFactory;
    private readonly string _connectionString;
    private readonly ILogger<DatabaseManager> _logger;

    private const string _databaseExistsQuery = "SELECT DB_ID(@dbName)";
    private const string _createDatabaseQuery = "EXEC('CREATE DATABASE ' + @dbName)";

    public DatabaseManager(
        DbProviderFactory dbProviderFactory,
        string connectionString,
        ILogger<DatabaseManager> logger)
    {
        _dbProviderFactory = dbProviderFactory ?? throw new ArgumentNullException(nameof(dbProviderFactory));
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InitializeDatabase(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Initializing database");
        
        await CreateDatabaseIfNotExists(cancellationToken);

        _logger.LogInformation("Database initialized");
    }

    private async Task CreateDatabaseIfNotExists(CancellationToken cancellationToken)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
        var dbName = connectionStringBuilder.InitialCatalog;

        if (string.IsNullOrWhiteSpace(dbName))
            throw new InvalidOperationException("Database name is missing.");

        connectionStringBuilder.InitialCatalog = string.Empty;

        await using var connection = await CreateConnection(connectionStringBuilder.ConnectionString, cancellationToken);

        var id = await connection.ExecuteScalarAsync(_databaseExistsQuery, new { dbName });

        if (id == null)
        {
            _logger.LogInformation("Creating database {databaseName}", dbName);

            await connection.ExecuteAsync(_createDatabaseQuery, new { dbName });
        }
        else
            _logger.LogInformation("Database {databaseName} already exists", dbName);
    }

    private async Task<DbConnection> CreateConnection(string connectionString, CancellationToken cancellationToken)
    {
        var connection = _dbProviderFactory.CreateConnection()
            ?? throw new InvalidOperationException("Failed to instantiate DbConnection object.");

        connection.ConnectionString = connectionString;

        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}
