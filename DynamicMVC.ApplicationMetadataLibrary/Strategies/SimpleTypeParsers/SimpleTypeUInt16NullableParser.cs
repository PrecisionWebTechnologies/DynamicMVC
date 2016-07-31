using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable UInt16 datatype
    /// </summary>
    public class SimpleTypeUInt16NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ushort?);
        }

        public dynamic Parse(string value)
        {
            return (ushort?)ushort.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.UInt32Nullable;
        }
    }
}
