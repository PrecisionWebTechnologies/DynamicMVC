using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Double datatype
    /// </summary>
    public class SimpleTypeDoubleNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(double?);
        }

        public dynamic Parse(string value)
        {
            return (double?)double.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.DoubleNullable;
        }
    }
}
