using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers
{
    /// <summary>
    /// Simple Type Parser for UInt16 datatype
    /// </summary>
    public class SimpleTypeUInt16Parser : ISimpleTypeParser
    {
        public Type GetSimpleType()
        {
            return typeof(ushort);
        }

        public dynamic Parse(string value)
        {
            return ushort.Parse(value);
        }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectionLibrary.Enums.SimpleTypeEnum.UInt16;
        }
    }
}
