using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Decimal datatype
    /// </summary>
    public class SimpleTypeDecimalParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(decimal);
        }

        public dynamic Parse(string value)
        {
            return decimal.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.Decimal;
        }
    }
}
