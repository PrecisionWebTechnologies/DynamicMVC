using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Double datatype
    /// </summary>
    public class SimpleTypeDoubleNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(double?);
        }

        public dynamic Parse(string value)
        {
            return (double?)double.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.DoubleNullable;
        }
    }
}
