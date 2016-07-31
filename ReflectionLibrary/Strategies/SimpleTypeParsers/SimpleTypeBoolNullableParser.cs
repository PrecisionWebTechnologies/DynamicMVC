using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for bool? datatype
    /// </summary>
    public class SimpleTypeBoolNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(bool?);
        }

        public dynamic Parse(string value)
        {
            return (bool?)bool.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.BoolNullable;
        }
    }
}
