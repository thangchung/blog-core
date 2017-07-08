using System;
using System.ComponentModel;

namespace BlogCore.Infrastructure.Extensions
{
    public static class TypeConversionExtensions
    {
        public static T ConvertTo<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                    return (T)converter.ConvertFromString(input);
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }
    }
}