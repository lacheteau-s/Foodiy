namespace Foodiy.Data;

public interface IDatabaseManager
{
    Task InitializeDatabase(CancellationToken cancellationToken = default);
}
