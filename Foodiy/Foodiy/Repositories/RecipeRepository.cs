using Foodiy.Models;
using SQLite;

namespace Foodiy.Repositories
{
    public class RecipeRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        private readonly Task _initialize;

        public RecipeRepository(SQLiteAsyncConnection connection)
        {
             _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _initialize = Initialize();
        }

        private async Task Initialize()
        {
            if (_initialize?.IsCompleted ?? false) return;

            await _connection.CreateTableAsync<RecipeModel>();
        }

        public async Task<IEnumerable<RecipeModel>> GetRecipes()
        {
            await _initialize;

            return await _connection.Table<RecipeModel>().ToListAsync();
        }

        public async Task AddRecipe(string name)
        {
            await _initialize;

            await _connection.InsertAsync(new RecipeModel
            {
                Name = name
            });
        }

        public async Task RemoveRecipe(int id)
        {
            await _initialize;

            await _connection.DeleteAsync<RecipeModel>(id);
        }
    }
}
