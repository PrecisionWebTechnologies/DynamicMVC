using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for String datatype
    /// </summary>
    public class SimpleTypeStringParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(string);
        }

        public dynamic Parse(string value)
        {
            return value;
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.String;
        }
    }
}
