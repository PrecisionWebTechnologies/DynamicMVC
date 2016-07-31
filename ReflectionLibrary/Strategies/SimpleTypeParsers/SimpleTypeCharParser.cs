using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Char datatype
    /// </summary>
    public class SimpleTypeCharParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(char);
        }

        public dynamic Parse(string value)
        {
            return char.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Char;
        }
    }
}
