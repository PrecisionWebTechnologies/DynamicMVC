using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Int64 datatype
    /// </summary>
    public class SimpleTypeInt64NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(long?);
        }

        public dynamic Parse(string value)
        {
            return (long?)long.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.Int64Nullable;
        }
    }
}
