﻿using Foodiy.Api.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Foodiy.Api.Services;

public class InMemoryRecipesProvider : IRecipesProvider
{
    private const string _key = "recipes";
    private readonly IMemoryCache _memoryCache;

    public InMemoryRecipesProvider(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public async Task<IEnumerable<RecipeModel>> GetRecipesAsync(CancellationToken cancellationToken = default)
    {
        if (_memoryCache.TryGetValue<IEnumerable<RecipeModel>>(_key, out var recipes))
            return recipes;

        recipes = await LoadRecipesFromFileAsync(cancellationToken);

        return _memoryCache.Set(_key, recipes);
    }

    private static async Task<IEnumerable<RecipeModel>> LoadRecipesFromFileAsync(CancellationToken cancellationToken = default)
    {
        using var stream = File.OpenRead("Data/recipes.json");
        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<RecipeModel>>(stream, cancellationToken: cancellationToken);

        return recipes ?? throw new IOException("Failed to load contents from recipes.json");
    }
}
