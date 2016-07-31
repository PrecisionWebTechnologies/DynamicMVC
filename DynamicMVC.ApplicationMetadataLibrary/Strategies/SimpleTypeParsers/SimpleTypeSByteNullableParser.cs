using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Nullable SByte datatype
    /// </summary>
    public class SimpleTypeSByteNullableParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(sbyte?);
        }

        public dynamic Parse(string value)
        {
            return (sbyte?)sbyte.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.SByteNullable;
        }
    }
}
