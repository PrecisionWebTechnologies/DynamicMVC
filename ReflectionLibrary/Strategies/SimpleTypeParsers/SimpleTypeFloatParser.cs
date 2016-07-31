using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Float datatype
    /// </summary>
    public class SimpleTypeFloatParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(float);
        }

        public dynamic Parse(string value)
        {
            return float.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Float;
        }
    }
}
