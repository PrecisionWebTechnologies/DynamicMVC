using System;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

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
            return Shared.Enums.SimpleTypeEnum.UInt16;
        }
    }
}
