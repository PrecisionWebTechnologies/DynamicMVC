using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable UInt64 datatype
    /// </summary>
    public class SimpleTypeUInt64NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ulong?);
        }

        public dynamic Parse(string value)
        {
            return (ulong?)ulong.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.UInt64Nullable;
        }
    }
}
