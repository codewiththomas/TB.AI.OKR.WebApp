using System.ComponentModel;
using System.Reflection;

namespace TB.AI.OKR.Core.Application;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        FieldInfo field = value.GetType().GetField(value.ToString());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
        DescriptionAttribute attribute =
            (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        return attribute == null ? value.ToString() : attribute.Description;
    }
}
