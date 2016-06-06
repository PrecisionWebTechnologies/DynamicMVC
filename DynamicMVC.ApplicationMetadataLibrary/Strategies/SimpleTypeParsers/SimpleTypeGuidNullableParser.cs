using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
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
            return Shared.Enums.SimpleTypeEnum.GuidNullable;
        }
    }
}
