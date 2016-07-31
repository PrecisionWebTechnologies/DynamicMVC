using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Byte datatype
    /// </summary>
    public class SimpleTypeByteNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(byte?);
        }

        public dynamic Parse(string value)
        {
            return (byte?)byte.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.ByteNullable;
        }
    }
}
