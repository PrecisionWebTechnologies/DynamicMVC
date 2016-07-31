using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Int64 datatype
    /// </summary>
    public class SimpleTypeInt64NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(long?);
        }

        public dynamic Parse(string value)
        {
            return (long?)long.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Int64Nullable;
        }
    }
}
