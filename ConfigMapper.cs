using System.Reflection;
using System.Text.RegularExpressions;

public static class ConfigMapper
{
    public static Configuration? Map(string text)
    {
        var dictionary = ConfigParser.Parse(text);
        var result = new Configuration();

        var confType = result.GetType();
        var properties = confType.GetProperties();

        foreach (var info in properties)
        {
            var nameToConfig = info.GetCustomAttribute<NameToConfigAttribute>()?.Name;

            if (nameToConfig is not null
                && dictionary.TryGetValue(nameToConfig, out var value))
            {
                info.SetValue(result, info.PropertyType == typeof(int)
                    ? int.Parse(value ?? "0")
                    : value);
            }
        }

        return result;
    }
}