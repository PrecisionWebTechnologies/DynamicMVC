using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Int32 datatype
    /// </summary>
    public class SimpleTypeInt32NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(int?);
        }

        public dynamic Parse(string value)
        {
            return (int?)int.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Int32Nullable;
        }
    }
}
