using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Foodiy.Data;

public class DatabaseManager : IDatabaseManager
{
    private readonly DbProviderFactory _dbProviderFactory;
    private readonly IFileProvider _sqlFileProvider;
    private readonly string _connectionString;
    private readonly ILogger<DatabaseManager> _logger;

    private static readonly Regex _sqlFileRegex = new(@"^[0-9]{4}(?:_[a-zA-Z0-9]+)+.sql$", RegexOptions.Compiled);

    private const string _databaseExistsQuery = "SELECT DB_ID(@dbName)";
    private const string _createDatabaseQuery = "EXEC('CREATE DATABASE ' + @dbName)";
    private const string _getVersionQuery = "SELECT MAX(version) FROM schema_version";

    public DatabaseManager(
        DbProviderFactory dbProviderFactory,
        string connectionString,
        IFileProvider sqlFileProvider,
        ILogger<DatabaseManager> logger)
    {
        _dbProviderFactory = dbProviderFactory ?? throw new ArgumentNullException(nameof(dbProviderFactory));
        _sqlFileProvider = sqlFileProvider ?? throw new ArgumentNullException( nameof(sqlFileProvider));
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InitializeDatabase(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Initializing database");
        
        await CreateDatabaseIfNotExists(cancellationToken);

        var currentVersion = await TryGetCurrentVersion(cancellationToken);

        CheckDatabaseVersion(currentVersion);
    
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

    private async Task<int?> TryGetCurrentVersion(CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(_connectionString, cancellationToken);

        try
        {
            var version = await connection.ExecuteScalarAsync<int?>(_getVersionQuery);

            return version ?? throw new InvalidOperationException("Failed to retrieve database version: table 'schema_version' is empty.");
        }
        catch (SqlException ex) when (ex.Number == 208)
        {
            return null;
        }
    }

    private void CheckDatabaseVersion(int? currentVersion)
    {
        var expectedVersion = GetScripts().Select(x => x.Version).LastOrDefault(-1);

        if (expectedVersion < 0)
            throw new InvalidOperationException("Unable to determine expected database version. No scripts found.");

        if (currentVersion == null || currentVersion < expectedVersion)
            _logger.LogWarning("Database if out of date. Expected version: {expectedVersion}. Current version: {currentVersion}.", expectedVersion, currentVersion);

        if (currentVersion == expectedVersion)
            _logger.LogInformation("Database is up to date. Version: {currentVersion}", currentVersion);

        if (currentVersion > expectedVersion)
            throw new InvalidOperationException($"Database version ({currentVersion}) is ahead of target ({expectedVersion}). The application was likely downgraded.");
    }

    private IEnumerable<(int Version, IFileInfo FileInfo)> GetScripts()
    {
        return _sqlFileProvider.GetDirectoryContents("")
            .Where(x => _sqlFileRegex.IsMatch(x.Name))
            .Select(x => (Version: int.Parse(x.Name[..4]), FileInfo: x))
            .OrderBy(x => x.Version);
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
