using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Int16 datatype
    /// </summary>
    public class SimpleTypeInt16NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(short?);
        }

        public dynamic Parse(string value)
        {
            return (short?)short.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Int16Nullable;
        }
    }
}
