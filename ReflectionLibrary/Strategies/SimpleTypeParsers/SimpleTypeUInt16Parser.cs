using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for UInt16 datatype
    /// </summary>
    public class SimpleTypeUInt16Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ushort);
        }

        public dynamic Parse(string value)
        {
            return ushort.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.UInt16;
        }
    }
}
