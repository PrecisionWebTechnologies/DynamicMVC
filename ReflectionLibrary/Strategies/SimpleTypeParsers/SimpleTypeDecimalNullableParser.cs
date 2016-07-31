using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Decimal datatype
    /// </summary>
    public class SimpleTypeDecimalNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(decimal?);
        }

        public dynamic Parse(string value)
        {
            return (decimal?)decimal.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.DecimalNullable;
        }
    }
}
