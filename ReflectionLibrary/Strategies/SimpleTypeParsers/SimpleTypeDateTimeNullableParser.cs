using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable DateTime datatype
    /// </summary>
    public class SimpleTypeDateTimeNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(DateTime?);
        }

        public dynamic Parse(string value)
        {
            return (DateTime?)DateTime.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.DateTimeNullable;
        }
    }
}
