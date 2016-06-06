using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Char datatype
    /// </summary>
    public class SimpleTypeCharNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(char?);
        }

        public dynamic Parse(string value)
        {
            return (char?)char.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return Shared.Enums.SimpleTypeEnum.CharNullable;
        }
    }
}
