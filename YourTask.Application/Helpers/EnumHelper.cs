using System.ComponentModel;
using System.Reflection;

namespace YourTask.Application.Helpers;

public static class EnumHelper
{
    public static IEnumerable<KeyValuePair<Enum, string>> GetEnumDescriptions<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
                   .Cast<T>()
                   .Select(e => new KeyValuePair<Enum, string>(e, GetEnumDescription(e)));
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
