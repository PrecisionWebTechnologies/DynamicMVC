using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Byte datatype
    /// </summary>
    public class SimpleTypeByteParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(byte);
        }

        public dynamic Parse(string value)
        {
            return byte.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Byte;
        }
    }
}
