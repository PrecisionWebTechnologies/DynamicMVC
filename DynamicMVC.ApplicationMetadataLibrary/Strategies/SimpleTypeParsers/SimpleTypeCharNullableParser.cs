using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

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
            return ReflectionLibrary.Enums.SimpleTypeEnum.CharNullable;
        }
    }
}
