using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for UInt64 datatype
    /// </summary>
    public class SimpleTypeUInt64Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ulong);
        }

        public dynamic Parse(string value)
        {
            return ulong.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.UInt64;
        }
    }
}
