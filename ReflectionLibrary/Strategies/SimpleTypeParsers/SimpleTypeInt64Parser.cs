using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Int64 datatype
    /// </summary>
    public class SimpleTypeInt64Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(long);
        }

        public dynamic Parse(string value)
        {
            return long.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Int64;
        }
    }
}
