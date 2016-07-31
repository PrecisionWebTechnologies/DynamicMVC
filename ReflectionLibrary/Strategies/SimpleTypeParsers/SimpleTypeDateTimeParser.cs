using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for DateTime datatype
    /// </summary>
    public class SimpleTypeDateTimeParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(DateTime);
        }

        public dynamic Parse(string value)
        {
            return DateTime.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.DateTime;
        }
    }
}
