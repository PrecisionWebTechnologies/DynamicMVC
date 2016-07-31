using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Int16 datatype
    /// </summary>
    public class SimpleTypeInt16Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(short);
        }

        public dynamic Parse(string value)
        {
            return short.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Int16;
        }
    }
}
