using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for bool? datatype
    /// </summary>
    public class SimpleTypeBoolNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(bool?);
        }

        public dynamic Parse(string value)
        {
            return (bool?)bool.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.BoolNullable;
        }
    }
}
