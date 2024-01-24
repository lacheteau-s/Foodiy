using Foodiy.App.Views;

namespace Foodiy.App.Configuration;

public static class RoutingConfiguration
{
    private static readonly IReadOnlyDictionary<string, Type> _mapping = new Dictionary<string, Type>
    {
        ["Recipe"] = typeof(RecipePage),
    };

    public static MauiAppBuilder ConfigureRouting(this MauiAppBuilder builder)
    {
        foreach (var mapping in _mapping)
            Routing.RegisterRoute(mapping.Key, mapping.Value);

        return builder;
    }
}
