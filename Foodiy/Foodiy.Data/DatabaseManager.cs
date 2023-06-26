using Microsoft.Extensions.Logging;

namespace Foodiy.Data;

public class DatabaseManager : IDatabaseManager
{
    private readonly string _connectionString;
    private readonly ILogger<DatabaseManager> _logger;

    public DatabaseManager(string connectionString, ILogger<DatabaseManager> logger)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}
