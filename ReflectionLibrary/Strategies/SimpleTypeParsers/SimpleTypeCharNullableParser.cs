using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Char datatype
    /// </summary>
    public class SimpleTypeCharNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(char?);
        }

        public dynamic Parse(string value)
        {
            return (char?)char.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.CharNullable;
        }
    }
}
