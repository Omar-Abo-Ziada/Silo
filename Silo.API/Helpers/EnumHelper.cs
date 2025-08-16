using System.ComponentModel;

namespace Silo.API.Helpers;

public static class EnumHelper
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault();

        return attribute?.Description ?? value.ToString();
    }

    public static IEnumerable<ItemListViewModel> ToItemList<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>()
                .Select(e => new ItemListViewModel(Convert.ToInt32(e), e.ToString()))
                .ToList();
    }

    public record ItemListViewModel(int ID, string Name);
}
