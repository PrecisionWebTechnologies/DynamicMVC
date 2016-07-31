using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for SByte datatype
    /// </summary>
    public class SimpleTypeSByteParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(sbyte);
        }

        public dynamic Parse(string value)
        {
            return sbyte.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.SByte;
        }
    }
}
