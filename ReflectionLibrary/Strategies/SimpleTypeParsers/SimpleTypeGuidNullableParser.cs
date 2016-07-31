using System;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Guid datatype
    /// </summary>
    public class SimpleTypeGuidNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(Guid?);
        }

        public dynamic Parse(string value)
        {
            return (Guid?)Guid.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Enums.SimpleTypeEnum.GuidNullable;
        }
    }
}
