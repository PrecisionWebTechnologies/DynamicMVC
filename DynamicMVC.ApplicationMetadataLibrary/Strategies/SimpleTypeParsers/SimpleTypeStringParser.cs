using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for String datatype
    /// </summary>
    public class SimpleTypeStringParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(string);
        }

        public dynamic Parse(string value)
        {
            return value;
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.String;
        }
    }
}
