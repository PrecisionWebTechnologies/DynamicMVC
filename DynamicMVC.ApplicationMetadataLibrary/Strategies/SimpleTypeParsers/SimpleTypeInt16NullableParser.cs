using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Int16 datatype
    /// </summary>
    public class SimpleTypeInt16NullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(short?);
        }

        public dynamic Parse(string value)
        {
            return (short?)short.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.Int16Nullable;
        }
    }
}
