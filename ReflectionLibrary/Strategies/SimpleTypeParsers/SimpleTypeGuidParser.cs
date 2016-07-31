using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Guid datatype
    /// </summary>
    public class SimpleTypeGuidParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(Guid);
        }

        public dynamic Parse(string value)
        {
            return Guid.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.Guid;
        }
    }
}
