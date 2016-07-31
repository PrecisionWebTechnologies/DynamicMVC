using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Float datatype
    /// </summary>
    public class SimpleTypeFloatNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(float?);
        }

        public dynamic Parse(string value)
        {
            return (float?)float.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.FloatNullable;
        }
    }
}
