using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for Int16 datatype
    /// </summary>
    public class SimpleTypeInt16Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(short);
        }

        public dynamic Parse(string value)
        {
            return short.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.Int16;
        }
    }
}
