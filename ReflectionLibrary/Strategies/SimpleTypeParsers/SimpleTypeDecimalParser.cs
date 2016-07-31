using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Decimal datatype
    /// </summary>
    public class SimpleTypeDecimalParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(decimal);
        }

        public dynamic Parse(string value)
        {
            return decimal.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Decimal;
        }
    }
}
