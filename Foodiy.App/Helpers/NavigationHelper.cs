namespace Foodiy.App.Helpers;

public static class NavigationHelper
{
    public static async Task NavigateTo<T>(IDictionary<string, object> parameters)
    {
        var typeName = typeof(T).Name;

        await Shell.Current.GoToAsync(typeName, parameters);
    }
}
