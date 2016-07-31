using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Double datatype
    /// </summary>
    public class SimpleTypeDoubleParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(double);
        }

        public dynamic Parse(string value)
        {
            return double.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Double;
        }
    }
}
