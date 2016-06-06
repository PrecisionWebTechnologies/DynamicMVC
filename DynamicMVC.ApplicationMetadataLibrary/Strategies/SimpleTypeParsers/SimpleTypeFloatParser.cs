using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Float datatype
    /// </summary>
    public class SimpleTypeFloatParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(float);
        }

        public dynamic Parse(string value)
        {
            return float.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.Float;
        }
    }
}
