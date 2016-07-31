using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
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
            return ReflectionLibrary.Enums.SimpleTypeEnum.Int32Nullable;
        }
    }
}
