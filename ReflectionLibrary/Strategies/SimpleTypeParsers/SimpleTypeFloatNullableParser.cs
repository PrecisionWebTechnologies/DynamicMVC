using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Float datatype
    /// </summary>
    public class SimpleTypeFloatNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(float?);
        }

        public dynamic Parse(string value)
        {
            return (float?)float.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.FloatNullable;
        }
    }
}
