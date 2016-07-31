using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for UInt32 datatype
    /// </summary>
    public class SimpleTypeUInt32Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(uint);
        }

        public dynamic Parse(string value)
        {
            return uint.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.UInt32;
        }
    }
}
