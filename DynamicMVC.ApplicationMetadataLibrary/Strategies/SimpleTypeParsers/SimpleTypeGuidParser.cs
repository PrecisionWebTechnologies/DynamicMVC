using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
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
            return ReflectionLibrary.Enums.SimpleTypeEnum.Guid;
        }
    }
}
