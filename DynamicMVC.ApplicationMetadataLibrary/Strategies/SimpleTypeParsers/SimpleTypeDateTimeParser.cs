using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for DateTime datatype
    /// </summary>
    public class SimpleTypeDateTimeParser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(DateTime);
        }

        public dynamic Parse(string value)
        {
            return DateTime.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.DateTime;
        }
    }
}
