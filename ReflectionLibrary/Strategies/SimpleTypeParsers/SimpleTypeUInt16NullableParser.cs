using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable UInt16 datatype
    /// </summary>
    public class SimpleTypeUInt16NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ushort?);
        }

        public dynamic Parse(string value)
        {
            return (ushort?)ushort.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.UInt32Nullable;
        }
    }
}
