using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable UInt64 datatype
    /// </summary>
    public class SimpleTypeUInt64NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ulong?);
        }

        public dynamic Parse(string value)
        {
            return (ulong?)ulong.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.UInt64Nullable;
        }
    }
}
