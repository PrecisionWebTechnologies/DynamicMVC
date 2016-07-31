using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Int32 datatype
    /// </summary>
    public class SimpleTypeInt32Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(int);
        }

        public dynamic Parse(string value)
        {
            return int.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Int32;
        }
    }
}
