using SQLite;

namespace Foodiy.Models;

public record RecipeModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; init; }

    public string Name { get; init; }
}
