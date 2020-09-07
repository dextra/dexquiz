using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DexQuiz.Core.Enums
{
    public static class EnumHelper
    {
        public static bool IsEnumValid(this Type enumType, string value) => 
            Enum.IsDefined(enumType, value);

        public static T ParseEnum<T>(string value) => 
            (T)Enum.Parse(typeof(T), value, true);

        public static IEnumerable<T> GetEnumValues<T>() =>
            Enum.GetValues(typeof(T)).Cast<T>();
    }
}
