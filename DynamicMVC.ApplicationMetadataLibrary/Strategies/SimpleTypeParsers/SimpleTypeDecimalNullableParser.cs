using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable Decimal datatype
    /// </summary>
    public class SimpleTypeDecimalNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(decimal?);
        }

        public dynamic Parse(string value)
        {
            return (decimal?)decimal.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.DecimalNullable;
        }
    }
}
